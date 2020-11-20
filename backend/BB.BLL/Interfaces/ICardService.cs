using System.Threading.Tasks;
using BB.Common.Dto;
using BB.Common.Dto.Card;
using BB.DAL.Entities;

namespace BB.BLL.Interfaces
{
    public interface ICardService
    {
        Task<CardDto> Register(CardCredentialsDto cardCredentials);
        Task<(CardDto card, string token)> Login(CardCredentialsDto cardCredentials);
    }
}