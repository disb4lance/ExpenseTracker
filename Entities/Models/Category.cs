using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        [Column("CategoryId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
        public string Name { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
