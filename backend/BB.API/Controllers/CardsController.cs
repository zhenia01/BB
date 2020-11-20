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
        public async Task<CardDto> Login([FromBody]CardCredentialsDto cardCredentials)
        {
            var (card, token) = await _cardService.Login(cardCredentials);
            Response.Headers["access-token"] = token;
            return card;
        }
    }
}