namespace WebAPI.Interfaces.Service
{
    public interface IAccountService
    {
        public Task<Account> AddAccount(Account account);
        public Task<IEnumerable<Account>> DeleteAccount(int accountId);
        public Task<Account> GetAccountById(int accountId);
        public Task<Account> UpdateAccount(int accountId, Account account);
        public Task<Account> GetAccount(int accountId);
    }
}
