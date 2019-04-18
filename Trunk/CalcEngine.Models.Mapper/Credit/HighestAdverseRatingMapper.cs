using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class HighestAdverseRatingMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_LIABILITY_HIGHEST_ADVERSE_RATING_Type _creditXml;
        private MapperConfiguration _config = null;

        public HighestAdverseRatingMapper(CREDITREPORTING_CREDIT_CREDIT_LIABILITY_HIGHEST_ADVERSE_RATING_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_HIGHEST_ADVERSE_RATING_Type, HighestAdverseRating>()
                .ForMember(dest => dest._DateRaw, opt => opt.MapFrom(src => src._Date));
            });
        }

        public HighestAdverseRating Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            HighestAdverseRating destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_HIGHEST_ADVERSE_RATING_Type, HighestAdverseRating>(_creditXml);
            return destination;
        }
    }
}
