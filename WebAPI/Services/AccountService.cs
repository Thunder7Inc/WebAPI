using AutoMapper;
using WebAPI.Exceptions.Account;
using WebAPI.Models.DTOs;
using WebAPI.Interfaces.Repository;
using WebAPI.Interfaces.Service;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AccountService : IAccountService
    {
        #region Private Fields

        private readonly IRepository<int, Account> _accountRepository;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AccountService(IRepository<int, Account> accountRepository, IMapper mapper,
            ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        public async Task<AccountReturnDTO> Add(AccountDTO accountDto)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDto);
                var res = await _accountRepository.Add(account);
                return _mapper.Map<AccountReturnDTO>(res);
            }
            catch (UnableToAddAccountException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("UnableToDoAction");
            }
        }

        public async Task<AccountReturnDTO> Get(int id)
        {
            try
            {
                var res = await _accountRepository.GetById(id);
                return _mapper.Map<AccountReturnDTO>(res);
            }
            catch (NoSuchAccountExistException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("UnableToDoAction");
            }
        }
    }
}