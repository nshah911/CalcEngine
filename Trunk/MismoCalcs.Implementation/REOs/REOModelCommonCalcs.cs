using CalcEngine.Models.Mismo;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.REOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Implementation.REOs
{
    public class REOModelCommonCalcs : IReoModelCommonCalcs
    {
        private readonly IEnumerable<ReoProperty> _reoProperties;

        public REOModelCommonCalcs(IEnumerable<ReoProperty> reoProperties)
        {
            _reoProperties = reoProperties;
        }

        public decimal Total(ReoPropertySubjectIndicator subjectIndicator)
        {
            decimal total = 0;    
            foreach(ReoProperty reoProperty in _reoProperties)
            {
                if(reoProperty._SubjectIndicator == subjectIndicator)
                {
                    total += reoProperty._LienInstallmentAmount + reoProperty._MaintenanceExpenseAmount;
                }
            }
            return total;
        }


        public decimal Total (ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator)
        {
            decimal total = 0;            
            return total;
        }

        public decimal TotalMaintainanceExpAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator)
        {
            decimal total = 0;
            foreach(ReoProperty reoProperty in _reoProperties)
            {
                if (reoProperty._SubjectIndicator == subjectIndicator && reoProperty._CurrentResidenceIndicator == currentResidenceIndicator)
                {
                    total += reoProperty._MaintenanceExpenseAmount;
                }
            }
            return total;
        }

        public decimal TotalMaintainanceExpAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, DispositionStatusType dispositionStatusType)
        {
            decimal total = 0;
            foreach (ReoProperty reoProperty in _reoProperties)
            {
                if (reoProperty._SubjectIndicator == subjectIndicator && reoProperty._CurrentResidenceIndicator == currentResidenceIndicator 
                    && reoProperty._DispositionStatusType == dispositionStatusType)
                {
                    total += reoProperty._MaintenanceExpenseAmount;
                }
            }
            return total;
        }

        public decimal Total(ReoPropertySubjectIndicator subjectIndicator, 
            ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, DispositionStatusType disposeType)
        {
            decimal total = 0;
            return total;
        }

        public decimal Total(ReoPropertySubjectIndicator subjectIndicator,
            ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType)
        {
            decimal total = 0;
            return total;
        }

        public decimal Total(ReoPropertySubjectIndicator subjectIndicator,
                    ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType, 
                    LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator)
        {
            HashSet<ReoProperty> setReoProperties = new HashSet<ReoProperty>();
            decimal total = 0;
            foreach (ReoProperty reoProperty in _reoProperties)
            {
                if (reoProperty._SubjectIndicator == subjectIndicator && reoProperty._CurrentResidenceIndicator == currentResidenceIndicator)
                {
                    if (reoProperty.LinkedLiabilities != null)
                    {
                        foreach (Liability liab in reoProperty.LinkedLiabilities)
                        {
                            if (liab._Type == liabilityType 
                                && liab._PayoffStatusIndicator == payoffIndicator 
                                && liab._ExclusionIndicator == exclusionIndicator)
                            {
                                total += liab._MonthlyPaymentAmount;
                            }
                        }
                    }
                }
            }
            return total;
        }

        public decimal Total(ReoPropertySubjectIndicator subjectIndicator,
                    ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType,
                    LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator, DispositionStatusType disposeType)
        {
            decimal total = 0;
            foreach (ReoProperty reoProperty in _reoProperties)
            {
                if (reoProperty._SubjectIndicator == subjectIndicator 
                    && reoProperty._CurrentResidenceIndicator == currentResidenceIndicator 
                    && reoProperty._DispositionStatusType == disposeType)
                {
                    if (reoProperty.LinkedLiabilities != null)
                    {
                        foreach (Liability liab in reoProperty.LinkedLiabilities)
                        {
                            if (liab._Type == liabilityType
                                && liab._PayoffStatusIndicator == payoffIndicator
                                && liab._ExclusionIndicator == exclusionIndicator)
                            {
                                total += liab._MonthlyPaymentAmount;
                            }
                        }
                    }
                }
            }
            return total;
        }

        public decimal TotalNetRental(ReoPropertySubjectIndicator subjectIndicator,
                    ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType,
                    LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator, DispositionStatusType disposeType)
        {
            decimal total = 0;
            foreach (ReoProperty reoProperty in _reoProperties)
            {
                if (reoProperty._SubjectIndicator == subjectIndicator
                    && reoProperty._CurrentResidenceIndicator == currentResidenceIndicator
                    && reoProperty._DispositionStatusType == disposeType)
                {
                    decimal TotalMonthlyPayment = 0;
                    if (reoProperty.LinkedLiabilities != null)
                    {
                        foreach (Liability liab in reoProperty.LinkedLiabilities)
                        {
                            if (liab._Type == liabilityType
                                && liab._PayoffStatusIndicator == payoffIndicator
                                && liab._ExclusionIndicator == exclusionIndicator)
                            {
                                TotalMonthlyPayment += liab._MonthlyPaymentAmount;
                            }
                        }
                    }
                    //total += reoProperty._RentalIncomeGrossAmount - (TotalMonthlyPayment + reoProperty._MaintenanceExpenseAmount);       
                }
            }
            return total;
        }

        public decimal TotalNetRental(ReoProperty ReoProperty, ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, LiabilityType liabilityType, LiabilityPayoffStatusIndicator payoffIndicator, LiabilityExclusionIndicator exclusionIndicator, DispositionStatusType disposeType)
        {
            decimal total = 0;
            if (ReoProperty._SubjectIndicator == subjectIndicator
                    && ReoProperty._CurrentResidenceIndicator == currentResidenceIndicator
                    && ReoProperty._DispositionStatusType == disposeType)
            {
                if (ReoProperty.LinkedLiabilities != null)
                {
                    foreach (Liability liab in ReoProperty.LinkedLiabilities)
                    {
                        if (liab._Type == liabilityType
                            && liab._PayoffStatusIndicator == payoffIndicator
                            && liab._ExclusionIndicator == exclusionIndicator)
                        {
                            total += liab._MonthlyPaymentAmount;
                        }
                    }
                }
            }
            return total;
        }

        public decimal TotalRentalIncomeGrossAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator)
        {
            decimal total = 0;
            foreach (ReoProperty reoProperty in _reoProperties)
            {
                if (reoProperty._SubjectIndicator == subjectIndicator && reoProperty._CurrentResidenceIndicator == currentResidenceIndicator)
                {
                    total += reoProperty._RentalIncomeGrossAmount;
                }
            }
            return total;
        }

        public decimal TotalRentalIncomeGrossAmount(ReoPropertySubjectIndicator subjectIndicator, ReoPropertyCurrentResidenceIndicator currentResidenceIndicator, DispositionStatusType dispositionType)
        {
            decimal total = 0;
            foreach (ReoProperty reoProperty in _reoProperties)
            {
                if (reoProperty._SubjectIndicator == subjectIndicator && reoProperty._CurrentResidenceIndicator == currentResidenceIndicator
                    && reoProperty._DispositionStatusType == dispositionType)
                {
                    total += reoProperty._RentalIncomeGrossAmount;
                }
            }
            return total;
        }


    }
}
