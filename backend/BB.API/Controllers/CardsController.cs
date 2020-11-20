using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.Common.Dto;
using Microsoft.AspNetCore.Mvc;


namespace BB.API.Controllers
{
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.Common.Dto;
using BB.Common.Dto.Card;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BB.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<CardDto> Register([FromBody]CardCredentialsDto cardCredentials)
        {
            return await _cardService.Register(cardCredentials);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<CardDto> Login([FromBody] CardCredentialsDto cardCredentials)
        {
            var (card, token) = await _cardService.Login(cardCredentials);
            Response.Headers["access-token"] = token;
            return card;
        }

        [HttpGet]
        public async Task<ReadOnlyCollection<CardDto>> GetAll()
        {
            return await _cardService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<CardDto> GetCardById(int id)
        {
            return await _cardService.GetCardById(id);
        }

        [HttpGet("card/{cardNum}")]
        public async Task<CardDto> GetCardByNum(string cardNum)
        {
            return await _cardService.GetCardByNum(cardNum);
        }
    }
}