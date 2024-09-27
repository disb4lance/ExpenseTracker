using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Expense
    {
        [Column("ExpenseId")]

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Amount is requared")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "PayMethod is requared")]
        public string PayMethod { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
