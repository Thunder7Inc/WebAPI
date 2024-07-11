using WebAPI.Models.Enums;

namespace WebAPI.Models.DTOs
{
    public class TransactionDTO
    {
        public int AccountId { get; set; }
        public TransactionType Type { get; set; } 
        public double Amount { get; set; }
    }
}