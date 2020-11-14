using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.Common.Dto;
using Microsoft.AspNetCore.Mvc;


namespace BB.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
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