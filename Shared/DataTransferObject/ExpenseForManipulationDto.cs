using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public abstract record ExpenseForManipulationDto
    {
        [Required(ErrorMessage = "Amount is requared")]
        public decimal Amount { get; init; }

        [Required(ErrorMessage = "PayMethod is requared")]
        public string PayMethod { get; init; }
    }
}
