using WebAPI.Models;

namespace WebAPI.Interfaces.Service
{
    public interface ITransactionService
    {
        public Task<Transaction> AddTransaction(Transaction transaction);
        public Task<IEnumerable<Transaction>> DeleteTransaction(int transactionId);
        public Task<Transaction> GetTransactionById(int transactionId);
        public Task<Transaction> UpdateTransaction(int transactionId, Transaction transaction);
        public Task<Transaction> GetTransaction(int transactionId);
    }
}
