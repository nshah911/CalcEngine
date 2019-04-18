using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class LateCountMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_LIABILITY_LATE_COUNT_Type _creditXml;
        private MapperConfiguration _config = null;

        public LateCountMapper(CREDITREPORTING_CREDIT_CREDIT_LIABILITY_LATE_COUNT_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_LATE_COUNT_Type, LateCount>();
            });
        }

        internal LateCount Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            LateCount destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_LATE_COUNT_Type, LateCount>(_creditXml);
            return destination;
        }
    }
}
