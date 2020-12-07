using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.BLL.Services.Abstract;
using BB.DAL.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper.QueryableExtensions;
using BC = BCrypt.Net.BCrypt;
using BB.Common.Dto.Card;
using BB.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BB.BLL.Services
{
    public class CardService : BaseService, ICardService
    {
        private readonly IConfiguration _configuration;
        
        public CardService(IConfiguration configuration, BBContext context, IMapper mapper) : base(context, mapper)
        {
            _configuration = configuration;
        }
        
        public async Task<CardDto> GetCardById(int id)
        {
            return await Context.Cards.AsNoTracking()
                .ProjectTo<CardDto>(Mapper.ConfigurationProvider)
                .SingleAsync(c => c.CardId  == id);
        }

        public async Task<CardDto> GetCardByNum(string cardNum)
        {
            return await Context.Cards.AsNoTracking()
                .ProjectTo<CardDto>(Mapper.ConfigurationProvider)
                .SingleAsync(c => c.Number == cardNum);
        }

        public async Task<ReadOnlyCollection<CardDto>> GetAll()
        {
            var cards = await Context.Cards.AsNoTracking()
                .ToListAsync();

            return Mapper.Map<ReadOnlyCollection<CardDto>>(cards);
        }
        
        public async Task<(CardDto card, string token)> Login(CardLoginDto cardLogin)
        {
            (string number, string pin) = cardLogin;
            
            Card card = await Context.Cards.SingleAsync(c => c.Number == number);

            if (!BC.Verify(pin, card.Pin))
            {
                throw new UnauthorizedAccessException("Wrong pin");
            }

            return (Mapper.Map<CardDto>(card), GenerateJwtToken(card.CardId));
        }

        public async Task<CardDto> Register(CardCredentialsDto cardCredentials)
        {
            (string number, string pin, int userId) = cardCredentials;
            
            Card card = new()
            {
                Number = number, Pin = BC.HashPassword(pin), UserId = userId,
                CheckingBranch = new CheckingBranch
                {   
                    Balance = 0m
                },
                User = await Context.Users.FindAsync(cardCredentials.UserId)
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
                Subject = new ClaimsIdentity(new[] { new Claim(JwtRegisteredClaimNames.Jti, cardId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}