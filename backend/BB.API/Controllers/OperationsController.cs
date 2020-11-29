using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.Common.Dto.Balance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BB.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class OperationsController : ControllerBase
    {
        private readonly ICheckingBranchService _branchService;

        public OperationsController(ICheckingBranchService checkingBranchService)
        {
            _branchService = checkingBranchService;
        }

        [HttpGet("balance/{cardId}")]
        public async Task<ActionResult<BalanceDto>> CheckBalance(int cardId)
        {
            if (User.HasClaim(JwtRegisteredClaimNames.Jti, cardId.ToString()))
            {
                return Ok(await _branchService.CheckBalance(cardId));
            }

            return Forbid();
        }
        
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(int cardId, decimal amount)
        {
            if (User.HasClaim(JwtRegisteredClaimNames.Jti, cardId.ToString()))
            {
                await _branchService.Withdraw(cardId, amount);
            }
            
            return Forbid();
        }

        [HttpPost("topup")]
        public async Task TopUp(int cardId, decimal amount)
        {
            await _branchService.TopUp(cardId, amount);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(int cardId, string targetCardNum, decimal amount)
        {
            if (User.HasClaim(JwtRegisteredClaimNames.Jti, cardId.ToString()))
            {
                await _branchService.Transfer(cardId, targetCardNum, amount);
            }
            
            return Forbid();
        }
    }
}