using CalcEngine.Models.Mismo;
using MismoCalcs.Interface.Incomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.Incomes
{
    public class IncomeCalcs : IIncomeCalcs
    {
        private readonly IIncomeModelCommonCalcs _incomeModelCommonCalcs = null;
        public IncomeCalcs(IIncomeModelCommonCalcs incomeModelCommonCalcs)
        {
            _incomeModelCommonCalcs = incomeModelCommonCalcs;
        }

        public decimal TotalBorrowerIncome()
        {
            decimal total = _incomeModelCommonCalcs.Total(IncomeType.Pension) * 1.15M
                + _incomeModelCommonCalcs.Total(IncomeType.SocialSecurity) * 1.15M
                + _incomeModelCommonCalcs.Total(IncomeType.AlimonyChildSupport)
                + _incomeModelCommonCalcs.Total(IncomeType.AssetBasedIncome)
                + _incomeModelCommonCalcs.Total(IncomeType.AutomobileExpenseAccount)
                + _incomeModelCommonCalcs.Total(IncomeType.BasePay)
                + _incomeModelCommonCalcs.Total(IncomeType.Bonuses)
                + _incomeModelCommonCalcs.Total(IncomeType.Bonus)
                + _incomeModelCommonCalcs.Total(IncomeType.CapitalGains)
                + _incomeModelCommonCalcs.Total(IncomeType.Commissions)
                + _incomeModelCommonCalcs.Total(IncomeType.DividendsInterest)
                + _incomeModelCommonCalcs.Total(IncomeType.EmploymentRelatedAssets)
                + _incomeModelCommonCalcs.Total(IncomeType.FNMBoarderIncome)
                + _incomeModelCommonCalcs.Total(IncomeType.FNMGovernmentMortgageCreditCertificate)
                + _incomeModelCommonCalcs.Total(IncomeType.ForeignIncome)
                + _incomeModelCommonCalcs.Total(IncomeType.FosterCare)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryBasePay)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryClothesAllowance)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryCombatPay)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryFlightPay)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryHazardPay)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryOverseasPay)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryPropPay)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryQuartersAllowance)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryRationsAllowance)
                + _incomeModelCommonCalcs.Total(IncomeType.MilitaryVariableHousingAllowance)
                + _incomeModelCommonCalcs.Total(IncomeType.MortgageDifferential)
                + _incomeModelCommonCalcs.Total(IncomeType.NetRentalIncome)
                + _incomeModelCommonCalcs.Total(IncomeType.NotesReceivableInstallment)
                + _incomeModelCommonCalcs.Total(IncomeType.OtherIncome)
                + _incomeModelCommonCalcs.Total(IncomeType.Overtime)
                + _incomeModelCommonCalcs.Total(IncomeType.RoyaltyPayment)
                + _incomeModelCommonCalcs.Total(IncomeType.SeasonalIncome)
                + _incomeModelCommonCalcs.Total(IncomeType.SubjectPropertyNetCashFlow)
                + _incomeModelCommonCalcs.Total(IncomeType.TemporaryLeave)
                + _incomeModelCommonCalcs.Total(IncomeType.TipIncome)
                + _incomeModelCommonCalcs.Total(IncomeType.Trust)
                + _incomeModelCommonCalcs.Total(IncomeType.Unemployment)
                + _incomeModelCommonCalcs.Total(IncomeType.VABenefitsNonEducational);

            return total;
        }
    }
}
