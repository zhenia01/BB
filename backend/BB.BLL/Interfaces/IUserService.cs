using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.Common.Dto;

namespace BB.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByCardNum(string num);
        Task<ReadOnlyCollection<UserDto>> GetAll();
    }
}