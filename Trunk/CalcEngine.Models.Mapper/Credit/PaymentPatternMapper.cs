using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class PaymentPatternMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PAYMENT_PATTERN_Type _creditXml;
        private MapperConfiguration _config = null;

        public PaymentPatternMapper(CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PAYMENT_PATTERN_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PAYMENT_PATTERN_Type, PaymentPattern>()
                .ForMember(dest => dest._StartDateRaw, opt => opt.MapFrom(src => src._StartDate));
            });
        }

        internal PaymentPattern Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            PaymentPattern destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PAYMENT_PATTERN_Type, PaymentPattern>(_creditXml);
            return destination;
        }
    }
}
