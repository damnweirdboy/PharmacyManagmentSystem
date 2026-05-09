using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagementSystem.Models
{
    /// <summary>
    /// Represents a medicine in the pharmacy inventory.
    /// Maps to the Medicines table in the database.
    /// </summary>
    [Table("Medicines")]
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicineId { get; set; }

        [Required]
        [MaxLength(100)]
        public string MedicineName { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime? ExpiryDate { get; set; }

        // Navigation property: one medicine has many sales
        public virtual ICollection<Sale> Sales { get; set; }

        public Medicine()
        {
            Sales = new HashSet<Sale>();
        }
    }
}
