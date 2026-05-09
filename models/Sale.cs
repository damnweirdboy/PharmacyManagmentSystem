using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Models
{
    /// <summary>
    /// Represents a sale transaction.
    /// Maps to the Sales table in the database.
    /// </summary>
    [Table("Sales")]
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int QuantitySold { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        // Navigation properties
        [ForeignKey("MedicineId")]
        public virtual Medicine Medicine { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
