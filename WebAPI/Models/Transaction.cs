using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Enums;

namespace WebAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
