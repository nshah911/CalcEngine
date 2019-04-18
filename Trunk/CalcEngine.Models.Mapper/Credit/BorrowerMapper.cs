using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;

namespace CalcEngine.Models.Mapper.Credit
{
    public class BorrowerMapper
    {
        private CREDITREPORTING_CREDIT_BORROWER_Type _creditXml;
        private MapperConfiguration _config = null;

        public BorrowerMapper(CREDITREPORTING_CREDIT_BORROWER_Type creditXml)
        {
            _creditXml =  creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_BORROWER_Type, Borrower>()
                .ForMember(dest => dest._Residence, opt => opt.Ignore());
            });
        }

        public Borrower Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            Borrower destination = iMapper.Map<CREDITREPORTING_CREDIT_BORROWER_Type, Borrower>(_creditXml);
            return destination;
        }
    }
}
