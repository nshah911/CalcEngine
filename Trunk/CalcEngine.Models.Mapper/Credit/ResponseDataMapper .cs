using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;

namespace CalcEngine.Models.Mapper.Credit
{
    public class ResponseDataMapper
    {
        private CREDITREPORTING_RESPONSE_DATA_Type _creditXml;
        private MapperConfiguration _config = null;

        public ResponseDataMapper(CREDITREPORTING_RESPONSE_DATA_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_RESPONSE_DATA_Type, ResponseData>();
            });
        }

        internal ResponseData Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            ResponseData destination = iMapper.Map<CREDITREPORTING_RESPONSE_DATA_Type, ResponseData>(_creditXml);
            return destination;
        }
    }
}
