using System.Threading.Tasks;
using BB.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BB.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CreditController : ControllerBase
    {
        private readonly ICreditBranchService _creditService;

        public CreditController(ICreditBranchService creditBranchService)
        {
            _creditService = creditBranchService;
        }
        
        [HttpPost("createCreditAccount")]
        public async Task CreateCreditAccount(int cardId)
        {
            await _creditService.CreateCreditAccount(cardId);
        }
        
        [HttpPost("deleteCreditAccount")]
        public async Task DeleteCreditAccount(int cardId)
        {
            await _creditService.DeleteCreditAccount(cardId);
        }

    }
}