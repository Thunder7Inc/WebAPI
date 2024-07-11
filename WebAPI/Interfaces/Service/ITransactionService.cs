using WebAPI.Models;
using WebAPI.Models.DTOs;

namespace WebAPI.Interfaces.Service
{
    public interface ITransactionService
    {
        public Task<TransactionReturnDto> AddTransaction(TransactionDTO transaction);
        public Task<TransactionReturnDto> GetTransactionById(int transactionId);
        public Task<IEnumerable<TransactionReturnDto>> GetTransactions();
    }

}
