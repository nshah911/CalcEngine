using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Credit;
using MismoCalcs.Interface.Liabilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Liabilities
{
    public class LiabilitiesCalcs : ILiabilityCalcs, ICreditLiabilityCalcs
    {
        private readonly ILiabilityModelCommonCalcs _liabilityModelCommonCalcs = null;
        private readonly ICreditLiabilityCalcs _creditLiabilityCalcs = null;

        public LiabilitiesCalcs(ILiabilityModelCommonCalcs liabilityModelCommonCalcs,
            ICreditLiabilityCalcs creditLiabilityCalcs)
        {
            _liabilityModelCommonCalcs = liabilityModelCommonCalcs;
            _creditLiabilityCalcs = creditLiabilityCalcs;
        }

        public decimal TotalMonthlyLiabilites(int minRemainingInstallmentMonths)
        {
            decimal total = _liabilityModelCommonCalcs.Total(LiabilityType.Alimony)
                + _liabilityModelCommonCalcs.Total(LiabilityType.JobRelatedExpenses)
                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

                + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, minRemainingInstallmentMonths)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N, minRemainingInstallmentMonths)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown, minRemainingInstallmentMonths)
                + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown, minRemainingInstallmentMonths);

            return total;
        }

        public decimal TotalMortgagePayment(IEnumerable<Liability> liabilities)
        {
            return _liabilityModelCommonCalcs.Total(LiabilityType.MortgageLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, liabilities)
                + _liabilityModelCommonCalcs.Total(LiabilityType.HELOC, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N, liabilities);
        }

        public decimal TotalLiabilitiesNotMortgageHeloc()
        {
            decimal total = _liabilityModelCommonCalcs.Total(LiabilityType.Alimony)
              + _liabilityModelCommonCalcs.Total(LiabilityType.JobRelatedExpenses)
              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.ChildSupport, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgementsAndLiens, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Installment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.LeasePayments, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Open30DayChargeAccount, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherLiability, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Revolving, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.SeparateMaintenanceExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.OtherExpense, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Taxes, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgmentsAndLiens, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgmentsAndLiens, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgmentsAndLiens, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.CollectionsJudgmentsAndLiens, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.DeferredStudentLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.DeferredStudentLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.DeferredStudentLoan, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.DeferredStudentLoan, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.BorrowerEstimatedTotalMonthlyLiabilityPayment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.BorrowerEstimatedTotalMonthlyLiabilityPayment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.BorrowerEstimatedTotalMonthlyLiabilityPayment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.BorrowerEstimatedTotalMonthlyLiabilityPayment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.DelinquentTaxes, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.DelinquentTaxes, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.DelinquentTaxes, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.DelinquentTaxes, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.FirstMortgageBeingFinanced, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.FirstMortgageBeingFinanced, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.FirstMortgageBeingFinanced, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.FirstMortgageBeingFinanced, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.Garnishments, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Garnishments, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Garnishments, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.Garnishments, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.UnionDues, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnionDues, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnionDues, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnionDues, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanInstallment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanInstallment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanInstallment, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanInstallment, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown)

              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanRevolving, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanRevolving, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.N)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanRevolving, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.Unknown)
              + _liabilityModelCommonCalcs.Total(LiabilityType.UnsecuredHomeImprovementLoanRevolving, LiabilityPayoffStatusIndicator.Unknown, LiabilityExclusionIndicator.Unknown);

            return total;
        }

        public int LatePaymentCount()
        {
            return _creditLiabilityCalcs.LatePaymentCount();
        }

        public int LatePaymentCount(AccountType accountType)
        {
            return _creditLiabilityCalcs.LatePaymentCount(accountType);
        }

        public int LatePaymentCount(AccountType accountType, PriorAdverseRatingType priorAdverseRatingType)
        {
            return _creditLiabilityCalcs.LatePaymentCount(accountType, priorAdverseRatingType);
        }

        public int LatePaymentCount(AccountType accountType, PriorAdverseRatingType priorAdverseRatingType, int withinMonths)
        {
            return _creditLiabilityCalcs.LatePaymentCount(accountType, priorAdverseRatingType, withinMonths);
        }
    }
}
