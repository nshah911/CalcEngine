using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class CurrentRatingMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_LIABILITY_CURRENT_RATING_Type _creditXml;
        private MapperConfiguration _config = null;

        public CurrentRatingMapper(CREDITREPORTING_CREDIT_CREDIT_LIABILITY_CURRENT_RATING_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_CURRENT_RATING_Type, CurrentRating>();
            });
        }

        public CurrentRating Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            CurrentRating destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_CURRENT_RATING_Type, CurrentRating>(_creditXml);
            return destination;
        }
    }
}
