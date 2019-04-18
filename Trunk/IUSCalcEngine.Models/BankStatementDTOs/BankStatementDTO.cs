using CalcEngine.Models.BankStatements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CalcEngine.Models.BankStatementDTOs
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false, ElementName = "BankStatement")]
    public partial class BankStatementDTO
    {
            private BANKSTATEMENT_DETAIL[] bANKSTATEMENT_DETAILField;

            private string[] textField;

            private string _BankStatementTypeField;

            private string _BankNameField;

            private string _AccountNumberField;

            private decimal _CurrentBalanceField;

            private decimal _AvgDepositAmtField;

            private decimal _ProfitLossRevenueField;

            private decimal _ProfitLossNetIncomeField;

            private string _ExpenseFactorTypeField;

            private decimal _CPAorTaxPreparerAmtField;

            private string _Min3MonthsBusinessBankStatementsField;

            private string _GrossDeposit75PctGrossRevenueField;

            private string _DepositsConsistentWithOccupationField;

            private string _EndingBalancesConsistentOrIncreasingField;

            private string _AbsentOfExcessiveOverDraftOrNSFField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("BANKSTATEMENT_DETAIL")]
            public BANKSTATEMENT_DETAIL[] BANKSTATEMENT_DETAIL
            {
                get
                {
                    return this.bANKSTATEMENT_DETAILField;
                }
                set
                {
                    this.bANKSTATEMENT_DETAILField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _BankStatementType
            {
                get
                {
                    return this._BankStatementTypeField;
                }
                set
                {
                    this._BankStatementTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _BankName
            {
                get
                {
                    return this._BankNameField;
                }
                set
                {
                    this._BankNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _AccountNumber
            {
                get
                {
                    return this._AccountNumberField;
                }
                set
                {
                    this._AccountNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _CurrentBalance
            {
                get
                {
                    return this._CurrentBalanceField;
                }
                set
                {
                    this._CurrentBalanceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _AvgDepositAmt
            {
                get
                {
                    return this._AvgDepositAmtField;
                }
                set
                {
                    this._AvgDepositAmtField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _ProfitLossRevenue
            {
                get
                {
                    return this._ProfitLossRevenueField;
                }
                set
                {
                    this._ProfitLossRevenueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _ProfitLossNetIncome
            {
                get
                {
                    return this._ProfitLossNetIncomeField;
                }
                set
                {
                    this._ProfitLossNetIncomeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _ExpenseFactorType
            {
                get
                {
                    return this._ExpenseFactorTypeField;
                }
                set
                {
                    this._ExpenseFactorTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _CPAorTaxPreparerAmt
            {
                get
                {
                    return this._CPAorTaxPreparerAmtField;
                }
                set
                {
                    this._CPAorTaxPreparerAmtField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _Min3MonthsBusinessBankStatements
            {
                get
                {
                    return this._Min3MonthsBusinessBankStatementsField;
                }
                set
                {
                    this._Min3MonthsBusinessBankStatementsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _GrossDeposit75PctGrossRevenue
            {
                get
                {
                    return this._GrossDeposit75PctGrossRevenueField;
                }
                set
                {
                    this._GrossDeposit75PctGrossRevenueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _DepositsConsistentWithOccupation
            {
                get
                {
                    return this._DepositsConsistentWithOccupationField;
                }
                set
                {
                    this._DepositsConsistentWithOccupationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _EndingBalancesConsistentOrIncreasing
            {
                get
                {
                    return this._EndingBalancesConsistentOrIncreasingField;
                }
                set
                {
                    this._EndingBalancesConsistentOrIncreasingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _AbsentOfExcessiveOverDraftOrNSF
            {
                get
                {
                    return this._AbsentOfExcessiveOverDraftOrNSFField;
                }
                set
                {
                    this._AbsentOfExcessiveOverDraftOrNSFField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class BANKSTATEMENT_DETAIL
        {

            private BANKSTATEMENTBANKSTATEMENT_DETAILBANKSTATEMENT_DETAIL_TRANSACTION[] bANKSTATEMENT_DETAIL_TRANSACTIONField;

            private int _StatementYearField;

            private int _StatementMonthField;

            private decimal _BeginningBalanceField;

            private decimal _DepositAmountField;

            private decimal _EndingBalanceField;
            
            private int _NSFCountField;

            public string NSFIndTypeId { get; set; }
            public string TransactionDetail { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BANKSTATEMENT_DETAIL_TRANSACTION")]
            public BANKSTATEMENTBANKSTATEMENT_DETAILBANKSTATEMENT_DETAIL_TRANSACTION[] BANKSTATEMENT_DETAIL_TRANSACTION
            {
                get
                {
                    return this.bANKSTATEMENT_DETAIL_TRANSACTIONField;
                }
                set
                {
                    this.bANKSTATEMENT_DETAIL_TRANSACTIONField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public int _StatementYear
            {
                get
                {
                    return this._StatementYearField;
                }
                set
                {
                    this._StatementYearField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public int _StatementMonth
            {
                get
                {
                    return this._StatementMonthField;
                }
                set
                {
                    this._StatementMonthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _BeginningBalance
            {
                get
                {
                    return this._BeginningBalanceField;
                }
                set
                {
                    this._BeginningBalanceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _DepositAmount
            {
                get
                {
                    return this._DepositAmountField;
                }
                set
                {
                    this._DepositAmountField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal _EndingBalance
            {
                get
                {
                    return this._EndingBalanceField;
                }
                set
                {
                    this._EndingBalanceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public int _NSFCount
            {
                get
                {
                    return this._NSFCountField;
                }
                set
                {
                    this._NSFCountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class BANKSTATEMENTBANKSTATEMENT_DETAILBANKSTATEMENT_DETAIL_TRANSACTION
        {

            private string _TransactionDateField;

            private string _DescriptionField;

            private ushort _DepositAmountField;

            private byte _TransactionTypeIdField;

            private byte _IsSelectedField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _TransactionDate
            {
                get
                {
                    return this._TransactionDateField;
                }
                set
                {
                    this._TransactionDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string _Description
            {
                get
                {
                    return this._DescriptionField;
                }
                set
                {
                    this._DescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort _DepositAmount
            {
                get
                {
                    return this._DepositAmountField;
                }
                set
                {
                    this._DepositAmountField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte _TransactionTypeId
            {
                get
                {
                    return this._TransactionTypeIdField;
                }
                set
                {
                    this._TransactionTypeIdField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte _IsSelected
            {
                get
                {
                    return this._IsSelectedField;
                }
                set
                {
                    this._IsSelectedField = value;
                }
            }
        }
}
