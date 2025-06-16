using Core.Entities.Abstractions;

namespace Core.Entities
{
    public class Transaction: BaseEntity
    {
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        // Navigation properties can be added if needed
        // public virtual User User { get; set; }
        // public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }

        // Additional properties or methods can be added as needed
    }
}
