﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record CategoryForUpdateDto(string Name, IEnumerable<ExpenseForUpdateDto> Expenses);

}
