using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class MostRecentAdverseRatingMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_LIABILITY_MOST_RECENT_ADVERSE_RATING_Type _creditXml;
        private MapperConfiguration _config = null;

        public MostRecentAdverseRatingMapper(CREDITREPORTING_CREDIT_CREDIT_LIABILITY_MOST_RECENT_ADVERSE_RATING_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_MOST_RECENT_ADVERSE_RATING_Type, MostRecentAdverseRating>()
                .ForMember(dest => dest._DateRaw, opt => opt.MapFrom(src => src._Date));
            });
        }

        public MostRecentAdverseRating Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            MostRecentAdverseRating destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_MOST_RECENT_ADVERSE_RATING_Type, MostRecentAdverseRating>(_creditXml);
            return destination;
        }
    }
}
