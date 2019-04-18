using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.BankStatementDTOs;
using CalcEngine.Models.BankStatements;

namespace CalcEngine.Models.Mapper.BankStatements
{
    public class BankStatementDetailsTransactionMapper
    {
        private BANKSTATEMENTBANKSTATEMENT_DETAILBANKSTATEMENT_DETAIL_TRANSACTION[] _bankTransactionDetailDTOs;
        private MapperConfiguration _config = null;

        public BankStatementDetailsTransactionMapper(BANKSTATEMENTBANKSTATEMENT_DETAILBANKSTATEMENT_DETAIL_TRANSACTION[] bankTransactionDetailDTOs)
        {
            _bankTransactionDetailDTOs = bankTransactionDetailDTOs;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BankStatementDetailTransactionDTO, BankStatementDetailTransaction>();
            });
        }

        public List<BankStatementDetailTransaction> Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            List<BankStatementDetailTransaction> destination = iMapper.Map<BANKSTATEMENTBANKSTATEMENT_DETAILBANKSTATEMENT_DETAIL_TRANSACTION[], List<BankStatementDetailTransaction>>(_bankTransactionDetailDTOs);
            return destination;
        }
    }
}
