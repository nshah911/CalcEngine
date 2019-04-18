using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class PriorAdverseRatingMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PRIOR_ADVERSE_RATING_Type[] _creditXml;
        private MapperConfiguration _config = null;

        public PriorAdverseRatingMapper(CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PRIOR_ADVERSE_RATING_Type[] creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PRIOR_ADVERSE_RATING_Type, PriorAdverseRating>()
                .ForMember(dest => dest._DateRaw, opt => opt.MapFrom(src => src._Date));
            });
        }

        internal List<PriorAdverseRating> Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            List<PriorAdverseRating> destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PRIOR_ADVERSE_RATING_Type[], List<PriorAdverseRating>>(_creditXml);
            return destination;
        }
    }
}
