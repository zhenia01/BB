using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.DAL.Context;
using AutoMapper;
using BB.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using AutoMapper;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.Common.Dto;
using BB.Common.Dto.Card;
using BB.DAL.Context;
using BB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BB.BLL.Services
{
    public class CardService : BaseService, ICardService
    {
        public CardService(BBContext context, IMapper mapper) : base(context, mapper) {}

        public async Task<CardDto> GetCardById(int id)
        {
            var card = await Context.Cards.AsNoTracking()
                .FirstAsync(c => c.CardId  == id);

            return Mapper.Map<CardDto>(card);
        }

        public async Task<CardDto> GetCardByNum(string cardNum)
        {
            var card = await Context.Cards.AsNoTracking()
                .FirstAsync(c => c.Number == cardNum);

            return Mapper.Map<CardDto>(card);
        }

        public async Task<ReadOnlyCollection<CardDto>> GetAll()
        {
            var cards = await Context.Cards.AsNoTracking()
                .ToListAsync();

            return Mapper.Map<ReadOnlyCollection<CardDto>>(cards);
        
        private readonly IConfiguration _configuration;
        
        public CardService(IConfiguration configuration, BBContext context, IMapper mapper) : base(context, mapper)
        {
            _configuration = configuration;
        }

        public async Task<(CardDto card, string token)> Login(CardCredentialsDto cardCredentials)
        {
            (string number, string pin) = cardCredentials;
            
            Card card = await Context.Cards.SingleOrDefaultAsync(c => c.Number == number);

            if (card == null || !BC.Verify(pin, card.Pin))
            {
                throw new Exception();
            }

            return (Mapper.Map<CardDto>(card), GenerateJwtToken(card.CardId));
        }

        public async Task<CardDto> Register(CardCredentialsDto cardCredentials)
        {
            (string number, string pin) = cardCredentials;
            
            Card card = new()
            {
                Number = number, Pin = BC.HashPassword(pin),
                CheckingBranch = new CheckingBranch
                {
                    Balance = 0m
                },
                User = await Context.Users.FindAsync(1)
            };
            
            await Context.AddAsync(card);
            await Context.SaveChangesAsync();

            return Mapper.Map<CardDto>(card);
        }
        
        private string GenerateJwtToken(int cardId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(new[] { new Claim("CardId", cardId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}