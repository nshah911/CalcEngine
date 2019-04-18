using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;

namespace CalcEngine.Models.Mapper.Credit
{
    public class CreditResponseMapper
    {
        private CREDITREPORTING_CREDIT_RESPONSE_Type cREDIT_RESPONSE;
        private MapperConfiguration _config = null;

        public CreditResponseMapper(CREDITREPORTING_CREDIT_RESPONSE_Type cREDIT_RESPONSE)
        {
            this.cREDIT_RESPONSE = cREDIT_RESPONSE;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_RESPONSE_Type, CreditResponse>();
            });
        }

        internal CreditResponse Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            CreditResponse destination = iMapper.Map<CREDITREPORTING_CREDIT_RESPONSE_Type, CreditResponse>(cREDIT_RESPONSE);
            return destination;
        }
    }
}
