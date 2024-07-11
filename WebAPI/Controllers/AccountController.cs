using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions.Account;
using WebAPI.Interfaces.Service;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.ErrorModels;

namespace WebAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Private Fields
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the AccountController.
        /// </summary>
        /// <param name="accountService">The injected account service.</param>
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }
        #endregion

        #region EndPoints

        /// <summary>
        /// Retrieves an account by its ID.
        /// </summary>
        /// <param name="accountId">The ID of the account to be retrieved.</param>
        /// <returns>An ActionResult containing the account details.</returns>
        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(AccountReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountReturnDTO>> GetAccountById(int accountId)
        {
            try
            {
                var result = await _accountService.Get(accountId);
                return Ok(result);
            }
            catch (NoSuchAccountExistException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        /// <summary>
        /// Adds a new account.
        /// </summary>
        /// <param name="account">The account object to be added.</param>
        /// <returns>An ActionResult containing the added account details.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(AccountReturnDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Account>> AddAccount([FromBody] AccountDTO account)
        {
            try
            {
                var result = await _accountService.Add(account);
                return CreatedAtAction(nameof(GetAccountById), new { accountId = result.Id }, result);
            }
            catch (UnableToAddAccountException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorModel(400, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        #endregion
    }

}