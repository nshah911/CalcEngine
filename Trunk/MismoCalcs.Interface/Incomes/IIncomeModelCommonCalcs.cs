﻿using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.Incomes
{
    public interface IIncomeModelCommonCalcs
    {
        decimal Total(IncomeType incomeType);
    }
}
