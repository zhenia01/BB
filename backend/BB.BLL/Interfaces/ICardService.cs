using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.Common.Dto;
using System.Threading.Tasks;
using BB.Common.Dto;
using BB.Common.Dto.Card;
using BB.DAL.Entities;

namespace BB.BLL.Interfaces
{
    public interface ICardService
    {
        Task<CardDto> GetCardById(int id);
        Task<CardDto> GetCardByNum(string cardNum);
        Task<ReadOnlyCollection<CardDto>> GetAll();
        Task<CardDto> Register(CardCredentialsDto cardCredentials);
        Task<(CardDto card, string token)> Login(CardCredentialsDto cardCredentials);
    }
}