using AutoMapper;
using WebAPI.Exceptions.Account;
using WebAPI.Exceptions.Transaction;
using WebAPI.Interfaces.Repository;
using WebAPI.Interfaces.Service;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.Enums;

namespace WebAPI.Services;

public class TransactionService : ITransactionService
{
    #region Private Fields

    private readonly IRepository<int, Transaction> _transactionRepository;
    private readonly IRepository<int, Account> _accountRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TransactionService> _logger;

    #endregion

    #region Constructor

    public TransactionService(IRepository<int, Transaction> transactionRepository, 
                              IRepository<int, Account> accountRepository,
                              IMapper mapper, 
                              ILogger<TransactionService> logger)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds a new transaction.
    /// </summary>
    /// <param name="transaction">The transaction object to be added.</param>
    /// <returns>The added transaction object.</returns>
    public async Task<TransactionReturnDto> AddTransaction(TransactionDTO transactionDto)
    {
        try
        {
            
            
            await ValidateTransaction(transactionDto);
            var transaction = _mapper.Map<Transaction>(transactionDto);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Define the IST time zone
            TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            // Convert the UTC time to IST
            DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istZone);

            // Assign the IST time to the transaction date
            transaction.Date = istNow;
            
            var accountId = transactionDto.AccountId;

            var account = await _accountRepository.GetById(accountId);

            if (transactionDto.PIN != account.PIN)
            {
                throw new InvalidPinException("Invalid PIN");
            }

            if (transaction.Type == TransactionType.Deposit)
            {
                account.Balance += transaction.Amount;
            }
            else if (transaction.Type == TransactionType.Withdrawl)
            {
                account.Balance -= transaction.Amount;
            }

            await _accountRepository.Update(account);
            
            var addedTransaction = await _transactionRepository.Add(transaction);
            return _mapper.Map<TransactionReturnDto>(addedTransaction);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new UnableToAddTransactionException($"Unable to add transaction: {ex.Message}");
        }
    }
    

    /// <summary>
    /// Retrieves a transaction by its ID.
    /// </summary>
    /// <param name="transactionId">The ID of the transaction.</param>
    /// <returns>The transaction object.</returns>
    public async Task<TransactionReturnDto> GetTransactionById(int transactionId)
    {
        try
        {
            var transaction = await _transactionRepository.GetById(transactionId);
            if (transaction == null)
            {
                throw new NoSuchTransactionExistException($"Transaction with ID {transactionId} does not exist.");
            }
            return _mapper.Map<TransactionReturnDto>(transaction);
        }
        catch (NoSuchTransactionExistException ex)
        {
            _logger.LogError(ex.Message);
            throw new NoSuchTransactionExistException(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception($"Unable to retrieve transaction: {ex.Message}");
        }
    }
    

    /// <summary>
    /// Retrieves all transactions.
    /// </summary>
    /// <returns>The list of transaction objects.</returns>
    public async Task<IEnumerable<TransactionReturnDto>> GetTransactions()
    {
        try
        {
            var transactions = await _transactionRepository.GetAll();
            if (!transactions.Any())
            {
                throw new NoSuchTransactionExistException();
            }
            return _mapper.Map<IEnumerable<TransactionReturnDto>>(transactions);
        }
        catch (NoSuchTransactionExistException ex)
        {
            _logger.LogError(ex.Message);
            throw new NoSuchTransactionExistException(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new Exception($"Unable to retrieve transactions: {ex.Message}");
        }
    }
    
    
    /// <summary>
    /// Retrieves a transaction by its account Id.
    /// </summary>
    /// <param name="accountID">The account ID of the Customer.</param>
    /// <returns>The transaction DTO object.</returns>
    public async Task<IEnumerable<TransactionReturnDto>> GetTransactionsByAccountId(int accountId)
    {
        var transactions = (await _transactionRepository.GetAll()).Where(t => t.AccountId == accountId);
        return _mapper.Map<IEnumerable<TransactionReturnDto>>(transactions);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Validates a transaction object.
    /// </summary>
    /// <param name="transaction">The transaction object to validate.</param>
    private async Task ValidateTransaction(TransactionDTO transaction)
    {
        // Validate Account
        var account = await _accountRepository.GetById(transaction.AccountId);
        if (account == null)
        {
            throw new NoSuchAccountExistException($"Account with ID {transaction.AccountId} does not exist.");
        }

        // Validate Amount
        if (transaction.Amount <= 0)
        {
            throw new InvalidTransactionAmountException("Transaction amount must be greater than zero.");
        }

        // Check for transaction type specific validations
        if (transaction.Type == TransactionType.Withdrawl)
        {
            if (transaction.Amount > account.Balance)
            {
                throw new InsufficientBalanceException("Insufficient balance for the transaction.");
            }
            if (transaction.Amount > 10000)
            {
                throw new TransactionLimitExceededException("Withdrawal amount exceeds the limit of 10,000.");
            }
        }
        else if (transaction.Type == TransactionType.Deposit)
        {
            if (transaction.Amount > 20000)
            {
                throw new TransactionLimitExceededException("Deposit amount exceeds the limit of 20,000.");
            }
        }
    }

    #endregion
}

