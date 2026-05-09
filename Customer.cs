using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Models
{
    /// <summary>
    /// Represents a customer who purchases medicines.
    /// Maps to the Customers table in the database.
    /// </summary>
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        // Navigation property: one customer has many sales
        public virtual ICollection<Sale> Sales { get; set; }

        public Customer()
        {
            Sales = new HashSet<Sale>();
        }
    }
}
