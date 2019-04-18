using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.BankStatementDTOs;
using CalcEngine.Models.BankStatements;

namespace CalcEngine.Models.Mapper.BankStatements
{
    public class BankStatementMapper
    {
        private BankStatementDTO _bankStatementXml;
        private MapperConfiguration _config = null;

        public BankStatementMapper(BankStatementDTO bankStatementXml)
        {
            _bankStatementXml = bankStatementXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BankStatementDTO, BankStatement>()
                .ForMember(dest => dest.BankStatementDetails, opt => opt.Ignore());
            });
        }

        public BankStatement Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            BankStatement destination = iMapper.Map<BankStatementDTO, BankStatement>(_bankStatementXml);
            return destination;
        }
    }
}
