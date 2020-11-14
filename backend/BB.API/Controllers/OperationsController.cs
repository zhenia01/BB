using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BB.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly ICheckingBranchService _branchService;

        public OperationsController(ICheckingBranchService checkingBranchService)
        {
            _branchService = checkingBranchService;
        }

        [HttpGet("{cardId}")]
        public async Task<BalanceDto> CheckBalance(int cardId)
        {
            return await _branchService.CheckBalance(cardId);
        }
        
        [HttpPut]
        public async Task Withdraw(int cardId, decimal amount)
        {
            await _branchService.Withdraw(cardId, amount);
        }

        [HttpPut]
        public async Task TopUp(int cardId, decimal amount)
        {
            await _branchService.TopUp(cardId, amount);
        }

        [HttpPut]
        public async Task Transfer(int cardId, string targetCardNum, decimal amount)
        {
            await _branchService.Transfer(cardId, targetCardNum, amount);
        }
    }
}