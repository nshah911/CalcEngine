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

namespace CalcEngine.Calcs.DocType.AgencyPlus
{
    public class AgencyPlusCalc : Calculations
    {
        public AgencyPlusCalc(AssetCalculation assetCalculation,
            CreditCalculation creditCalculation,
            LiabilitiesCalculation liabilitiesCalculation,
            ProposedHousingExpenseCalculation proposedHousingExpenseCalculation,
            LoanCalculation loanCalculation,
            ReoCalculation reoCalculation,
            TransactionDetailCalculation transactionDetailCalculation)
        {
            base.AssetCalculation = assetCalculation;
            base.CreditCalcs = creditCalculation;
            base.LiabilitiesCalcs = liabilitiesCalculation;
            base.LoanCalcs = loanCalculation;
            base.ProposedHousingCalcs = proposedHousingExpenseCalculation;
            base.ReoCalcs = reoCalculation;
            base.TransactionDetailCalcs = transactionDetailCalculation;
        }
    }
}
