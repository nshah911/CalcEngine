using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;

namespace CalcEngine.Models.Mapper.Credit
{
    public class ResponseMapper
    {
        private CREDITREPORTING_RESPONSE_Type _creditXml;
        private MapperConfiguration _config = null;

        public ResponseMapper(CREDITREPORTING_RESPONSE_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_RESPONSE_Type, Response>();
            });
        }

        internal Response Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            Response destination = iMapper.Map<CREDITREPORTING_RESPONSE_Type, Response>(_creditXml);
            return destination;
        }
    }
}
