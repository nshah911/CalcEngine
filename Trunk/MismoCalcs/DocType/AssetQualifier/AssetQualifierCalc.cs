using MismoCalcs.Interface.Assets;
using System;
using System.Collections.Generic;
using System.Text;
using CalcEngine.Models.Mismo;
using CalcEngine.Models;
using MismoCalcs.Interface;
using CalcEngine.Calcs.Assets;
using MismoCalcs.Interface.Loan;
using CalcEngine.Calcs.Loan;
using MismoCalcs.Interface.TransactionDetail;
using MismoCalcs.Interface.ProposedHousing;
using MismoCalcs.Interface.Liabilities;
using CalcEngine.Calcs.Liabilities;
using MismoCalcs.Interface.REOs;
using CalcEngine.Calcs.Reo;
using MismoCalcs.Interface.Credit;
using CalcEngine.Calcs.Credit;
using CalcEngine.Calcs.ProposedHousing;
using CalcEngine.Calcs.TransactionDetail;

namespace CalcEngine.Calcs.DocType.AssetQualifier
{
    /// <summary>
    /// This class will expose all the calculations related to an AssetQulifier DocType.
    /// </summary>
    public sealed class AssetQualifierCalc : Calculations
    {
        public AssetQualifierCalc(AssetCalculation assetCalcs,
            CreditCalculation creditCalcs,
            LiabilitiesCalculation liabilitiesCalcs,
            LoanCalculation loanCalcs,
            ProposedHousingExpenseCalculation proposedHousingCalcs,
            ReoCalculation reoCalcs,
            TransactionDetailCalculation transactionDetailCalcs)
        {
            base.AssetCalculation = assetCalcs;
            base.CreditCalcs = creditCalcs;
            base.LiabilitiesCalcs = liabilitiesCalcs;
            base.LoanCalcs = loanCalcs;
            base.ProposedHousingCalcs = proposedHousingCalcs;
            base.ReoCalcs = reoCalcs;
            base.TransactionDetailCalcs = transactionDetailCalcs;
        }
    }
}