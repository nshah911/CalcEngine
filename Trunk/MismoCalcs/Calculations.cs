using CalcEngine.Calcs.Assets;
using CalcEngine.Calcs.Credit;
using CalcEngine.Calcs.Liabilities;
using CalcEngine.Calcs.Loan;
using CalcEngine.Calcs.ProposedHousing;
using CalcEngine.Calcs.Reo;
using CalcEngine.Calcs.TransactionDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Calcs
{
    public class Calculations
    { 
        public AssetCalculation AssetCalculation { get; protected set; }

        public CreditCalculation CreditCalcs { get; protected set; }

        public LiabilitiesCalculation LiabilitiesCalcs { get; protected set; }

        public LoanCalculation LoanCalcs { get; protected set; }

        public ProposedHousingExpenseCalculation ProposedHousingCalcs { get; protected set; }

        public ReoCalculation ReoCalcs { get; protected set; }

        public TransactionDetailCalculation TransactionDetailCalcs { get; protected set; }

        public string Message { get; set; }
    }
}
