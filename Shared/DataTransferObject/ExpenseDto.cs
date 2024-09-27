using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record ExpenseDto(Guid Id, decimal Amout, string PayMethod);

}
