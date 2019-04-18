using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using CalcEngine.Models.BankStatementDTOs;
using CalcEngine.Models.BankStatements;

namespace CalcEngine.Models.Mapper.BankStatements
{
    public class BankStatementMapManager
    {
        private BankStatementDTO _bankStatementXml;
        private MapperConfiguration _config = null;

        public BankStatementMapManager(BankStatementDTO bankStatementXml)
        {
            _bankStatementXml = bankStatementXml;
        }
        public BankStatement MapToBankStatement()
        {
            BankStatement bankStatement = new BankStatement();
            if(_bankStatementXml != null)
            {
                bankStatement = MapBankStatement(_bankStatementXml);
                bankStatement.BankStatementDetails = new List<BankStatementDetails>();
                if(_bankStatementXml.BANKSTATEMENT_DETAIL != null)
                {
                    foreach (BANKSTATEMENT_DETAIL bkdDTO in _bankStatementXml.BANKSTATEMENT_DETAIL)
                    {
                        BankStatementDetails statementDetails = new BankStatementDetails();
                        statementDetails = MapBankStatementDetail(bkdDTO);
                        if (bkdDTO.BANKSTATEMENT_DETAIL_TRANSACTION != null)
                        {
                            statementDetails.BankStatementDetailTransactions = new List<BankStatementDetailTransaction>();
                            statementDetails.BankStatementDetailTransactions = MapBankStatementTransDetail(bkdDTO.BANKSTATEMENT_DETAIL_TRANSACTION);
                        }
                        bankStatement.BankStatementDetails.Add(statementDetails);
                    }
                }
            }
            return bankStatement;
        }

        private List<BankStatementDetailTransaction> MapBankStatementTransDetail(BANKSTATEMENTBANKSTATEMENT_DETAILBANKSTATEMENT_DETAIL_TRANSACTION[] bankTransactionDetailDTOs)
        {
            List<BankStatementDetailTransaction> bankStatementDetailTransactions = new List<BankStatementDetailTransaction>();
            if(bankTransactionDetailDTOs != null)
            {
                BankStatementDetailsTransactionMapper map = new BankStatementDetailsTransactionMapper(bankTransactionDetailDTOs);
                bankStatementDetailTransactions = map.Convert();
            }
            return bankStatementDetailTransactions;
        }

        private BankStatementDetails MapBankStatementDetail(BANKSTATEMENT_DETAIL bkdDTO)
        {
            BankStatementDetails bankStatementDetails = new BankStatementDetails();
            if(bkdDTO != null)
            {
                BankStatementDetailsMapper map = new BankStatementDetailsMapper(bkdDTO);
                bankStatementDetails = map.Convert();
            }
            return bankStatementDetails;
        }

        private BankStatement MapBankStatement(BankStatementDTO bankStatementXml)
        {
            BankStatement bankStatement = new BankStatement();
            if(bankStatementXml != null)
            {
                BankStatementMapper map = new BankStatementMapper(bankStatementXml);
                bankStatement = map.Convert();
            }
            return bankStatement;
        }
    }
}
