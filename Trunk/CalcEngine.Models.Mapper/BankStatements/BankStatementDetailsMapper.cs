using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.BankStatementDTOs;
using CalcEngine.Models.BankStatements;

namespace CalcEngine.Models.Mapper.BankStatements
{
    public class BankStatementDetailsMapper
    {
        private BANKSTATEMENT_DETAIL _bkdDTO;
        private MapperConfiguration _config = null;

        public BankStatementDetailsMapper(BANKSTATEMENT_DETAIL bkdDTO)
        {
            _bkdDTO = bkdDTO;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BANKSTATEMENT_DETAIL, BankStatementDetails>()
                .ForMember(dest => dest.BeginningBalance, opt => opt.MapFrom(src => src._BeginningBalance))
                .ForMember(dest => dest.DepositAmount, opt => opt.MapFrom(src => src._DepositAmount))
                .ForMember(dest => dest.EndingBalance, opt => opt.MapFrom(src => src._EndingBalance))
                .ForMember(dest => dest.NSFCount, opt => opt.MapFrom(src => src._NSFCount))
                .ForMember(dest => dest.NSFIndTypeId, opt => opt.MapFrom(src => src.NSFIndTypeId))
                .ForMember(dest => dest.StatementMonth, opt => opt.MapFrom(src => src._StatementMonth))
                .ForMember(dest => dest.StatementYear, opt => opt.MapFrom(src => src._StatementYear))
                .ForMember(dest => dest.TransactionDetail, opt => opt.Ignore())
                .ForMember(dest => dest.BankStatementDetailTransactions, opt => opt.Ignore());
            });
        }

        public BankStatementDetails Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            BankStatementDetails destination = iMapper.Map<BANKSTATEMENT_DETAIL, BankStatementDetails>(_bkdDTO);
            return destination;
        }
    }
}
