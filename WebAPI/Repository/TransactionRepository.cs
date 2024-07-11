using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces.Repository;
using WebAPI.Models;
using WebAPI.Contexts;
using WebAPI.Exceptions.Transaction;

namespace WebAPI.Repository
{
    public class TransactionRepository : IRepository<int, Transaction>
    {
        #region Fields

        private readonly AtmAppContext _context;

        #endregion

        #region Constructor

        #region Summary

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>

        #endregion

        public TransactionRepository(AtmAppContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        #region Add a Transaction

        /// <summary>
        /// Adds a new transaction to the database.
        /// </summary>
        /// <param name="item">The transaction object to be added.</param>
        /// <returns>Returns the newly added transaction object.</returns>
        public async Task<Transaction> Add(Transaction item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Add(item);
            int noOfRowsUpdated = await _context.SaveChangesAsync();

            if (noOfRowsUpdated <= 0)
            {
                throw new UnableToAddTransactionException("Something went wrong while adding a transaction");
            }

            return item;
        }

        #endregion

        #region Delete a Transaction

        #region Summary

        /// <summary>
        /// Deletes an existing transaction from the database.
        /// </summary>
        /// <param name="key">The transaction ID to be deleted.</param>
        /// <returns>Returns the deleted transaction object.</returns>

        #endregion

        public async Task<Transaction> Delete(int key)
        {
            var transaction = await GetById(key);

            if (transaction == null)
            {
                throw new NoSuchTransactionExistException($"Transaction with ID {key} doesn't exist!");
            }

            _context.Remove(transaction);
            int noOfRowsUpdated = await _context.SaveChangesAsync();

            return noOfRowsUpdated <= 0
                ? throw new UnableToDeleteTransactionException(
                    $"Something went wrong while deleting a transaction with ID {transaction.Id}")
                : transaction;
        }

        #endregion

        #region Get All Transactions

        #region Summary

        /// <summary>
        /// Gets all transactions from the database.
        /// </summary>
        /// <returns>Returns all transaction objects as an IEnumerable collection.</returns>

        #endregion

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            var transactions = await _context.Transactions.ToListAsync();
            return transactions;
        }

        #endregion

        #region Get Transaction By Id

        #region Summary

        /// <summary>
        /// Gets a specific transaction that matches the given ID.
        /// </summary>
        /// <param name="key">The transaction ID.</param>
        /// <returns>Returns the transaction object with the matching ID.</returns>

        #endregion

        public async Task<Transaction> GetById(int key)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == key);

            return transaction == null
                ? throw new NoSuchTransactionExistException($"Transaction with ID {key} doesn't exist!")
                : transaction;
        }

        #endregion

        #region Update a Transaction

        #region Summary

        /// <summary>
        /// Updates an existing transaction in the database.
        /// </summary>
        /// <param name="item">The transaction object to be updated.</param>
        /// <returns>Returns the updated transaction object.</returns>

        #endregion

        public async Task<Transaction> Update(Transaction item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var transaction = await GetById(item.Id);

            if (transaction == null)
            {
                throw new NoSuchTransactionExistException($"Transaction with ID {item.Id} doesn't exist!");
            }

            _context.Update(item);
            int noOfRowsUpdated = await _context.SaveChangesAsync();

            return noOfRowsUpdated <= 0
                ? throw new UnableToUpdateTransactionException(
                    $"Something went wrong while updating a transaction with ID {transaction.Id}")
                : transaction;
        }

        #endregion

        #endregion
    }
}

