using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BB.Common.Dto;

namespace BB.BLL.Interfaces
{
    public interface ICardService
    {
        Task<CardDto> GetCardById(int id);
        Task<CardDto> GetCardByNum(string cardNum);
        Task<ReadOnlyCollection<CardDto>> GetAll();
    }
}