using System.Collections.ObjectModel;
using BB.BLL.Interfaces;
using BB.Common.Dto.Card;
using System.Threading.Tasks;
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
        public async Task<CardDto> Login([FromBody] CardLoginDto cardLogin)
        {
            var (card, token) = await _cardService.Login(cardLogin);
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