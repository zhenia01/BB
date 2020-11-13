using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.BLL.Interfaces;
using BB.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BB.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ReadOnlyCollection<UserDto>> GetAll()
        {
            return await _userService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetById(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpGet("card/{cardId}")]
        public async Task<UserDto> GetByCardId(int cardId)
        {
            return await _userService.GetUserByCardId(cardId);
        }
    }
}