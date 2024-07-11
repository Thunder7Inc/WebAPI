using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions.Account;
using WebAPI.Exceptions.Transaction;
using WebAPI.Interfaces.Service;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.ErrorModels;
namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    #region Private Fields

    private readonly ITransactionService _transactionService;
    private readonly ILogger<TransactionController> _logger;

    #endregion

    #region Constructor

    public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds a new transaction.
    /// </summary>
    /// <param name="transactionDTO">The transaction data transfer object.</param>
    /// <returns>The added transaction object.</returns>
    [HttpPost]
    public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transactionDTO)
    {
        try
        {
            var addedTransaction = await _transactionService.AddTransaction(transactionDTO);
            return Ok(addedTransaction);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { Message = ex.Message });
        }
    }
    

    /// <summary>
    /// Retrieves a transaction by its ID.
    /// </summary>
    /// <param name="transactionId">The ID of the transaction.</param>
    /// <returns>The transaction object.</returns>
    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransactionById(int transactionId)
    {
        try
        {
            var transaction = await _transactionService.GetTransactionById(transactionId);
            return Ok(transaction);
        }
        catch (NoSuchTransactionExistException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { Message = ex.Message });
        }
    }

    
    /// <summary>
    /// Retrieves all transactions.
    /// </summary>
    /// <returns>The list of transaction objects.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        try
        {
            var transactions = await _transactionService.GetTransactions();
            return Ok(transactions);
        }
        catch (NoSuchTransactionExistException ex)
        {
            _logger.LogError(ex.Message);
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(new { Message = ex.Message });
        }
    }

    #endregion
}
