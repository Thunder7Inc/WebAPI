using WebAPI.Interfaces.Repository;
using WebAPI.Models;
using WebAPI.Contexts;
using WebAPI.Exceptions.Account;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repository;

public class AccountRepository : IRepository<int, Account>
{
    #region Fields

    private readonly AtmAppContext _context;

    #endregion

    #region Constructor
    #region Summary
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    #endregion
    public AccountRepository(AtmAppContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    #region Add an Account
    /// <summary>
    /// Adds a new account to the database.
    /// </summary>
    /// <param name="item">The account object to be added.</param>
    /// <returns>Returns the newly added account object.</returns>
    public async Task<Account> Add(Account item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _context.Add(item);
        int noOfRowsUpdated = await _context.SaveChangesAsync();

        if (noOfRowsUpdated <= 0)
        {
            throw new UnableToAddAccountException("okay");
        }

        return item;
    }

    #endregion

    #region Delete an Account
    #region Summary
    /// <summary>
    /// Deletes an existing account from the database.
    /// </summary>
    /// <param name="key">The account ID to be deleted.</param>
    /// <returns>Returns the deleted account object.</returns>
    #endregion
    public async Task<Account> Delete(int key)
    {
        var account = await GetById(key);

        if (account == null)
        {
            throw new NoSuchAccountExistException($"Account with ID {key} doesn't exist!");
        }

        _context.Remove(account);
        int noOfRowsUpdated = await _context.SaveChangesAsync();

        return noOfRowsUpdated <= 0
            ? throw new UnableToDeleteAccountException($"Something went wrong while deleting an account with ID {account.Id}")
            : account;
    }

    #endregion

    #region Get All Accounts
    #region Summary
    /// <summary>
    /// Gets all accounts from the database.
    /// </summary>
    /// <returns>Returns all account objects as an IEnumerable collection.</returns>
    #endregion
    public async Task<IEnumerable<Account>> GetAll()
    {
        var accounts = await _context.Accounts.ToListAsync();
        return accounts;
    }

    #endregion

    #region Get Account By Id
    #region Summary
    /// <summary>
    /// Gets a specific account that matches the given ID.
    /// </summary>
    /// <param name="key">The account ID.</param>
    /// <returns>Returns the account object with the matching ID.</returns>
    #endregion
    public async Task<Account> GetById(int key)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == key);

        return account == null ? throw new NoSuchAccountExistException($"Account with ID {key} doesn't exist!")
            : account;
    }

    #endregion

    #region Update an Account
    #region Summary
    /// <summary>
    /// Updates an existing account in the database.
    /// </summary>
    /// <param name="item">The account object to be updated.</param>
    /// <returns>Returns the updated account object.</returns>
    #endregion
    public async Task<Account> Update(Account item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var account = await GetById(item.Id);

        if (account == null)
        {
            throw new NoSuchAccountExistException($"Account with ID {item.Id} doesn't exist!");
        }

        _context.Update(item);
        int noOfRowsUpdated = await _context.SaveChangesAsync();

        return noOfRowsUpdated <= 0
            ? throw new UnableToUpdateAccountException($"Something went wrong while updating an account with ID {account.Id}")
            : account;
    }

    #endregion

    #endregion
}


