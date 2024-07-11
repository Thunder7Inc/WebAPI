using AutoMapper;
using WebAPI.Exceptions.Transaction;
using WebAPI.Interfaces.Repository;
using WebAPI.Interfaces.Service;
using WebAPI.Models;
using WebAPI.Models.DTOs;

namespace WebAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly IRepository<int, Transaction> _transactionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountService> _logger;

    public TransactionService(IRepository<int, Transaction> transactionRepository, IMapper mapper,
        ILogger<AccountService> logger)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _logger = logger;

    }
    public async Task<TransactionReturnDto> Add(TransationDTO transationDto)
    {
        try
        {
            var transation = _mapper.Map<Transaction>(transationDto);
            var res = await _transactionRepository.Add(transation);
            return _mapper.Map<TransactionReturnDto>(res);
        }
        catch (UnableToAddTransactionException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("UnableToDoAction");
        }
    }

    public async Task<TransactionReturnDto> Get(int id)
    {
        try
        {
            var res = await _transactionRepository.GetById(id);
            return _mapper.Map<TransactionReturnDto>(res);
        }
        catch (NoSuchTransactionExistException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("UnableToDoAction");
        }
    }
}