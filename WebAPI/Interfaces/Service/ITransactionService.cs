using WebAPI.Models;
using WebAPI.Models.DTOs;

namespace WebAPI.Interfaces.Service
{
    public interface ITransactionService
    {
        public Task<TransactionReturnDto> Add(TransationDTO transationDto);
        public Task<TransactionReturnDto> Get(int id);
    }
}
