using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class Borrower
    {
        public string BorrowerID { get; set; }
        public string JointAssetBorrowerID { get; set; }
        public string _FirstName { get; set; }
        public string _MiddleName { get; set; }
        public string _LastName { get; set; }
        public string _SSN { get; set; }
        public List<Income> _Incomes { get; set; }
        public List<PresentHousingExpense> PresentHousingExpenses { get; set; }
        public List<Declaration> _Declaration { get; set; }
    }
}
