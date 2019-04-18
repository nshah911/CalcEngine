using AutoMapper;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class LoanProductDataMapper
    {
        private LoanProductDataDTO _mismo231 = null;
        private MapperConfiguration _config = null;
 
        public LoanProductDataMapper(LoanProductDataDTO mismo231)
        {
            _mismo231 = mismo231;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RateAdjustmentDTO, RateAdjustment>()
                .ForMember(dest => dest.DurationMonths, opt => opt.MapFrom(src => src._DurationMonths));

                cfg.CreateMap<LoanProductDataDTO, LoanProductData>()
                .ForMember(dest => dest.LoanFeatures, opt => opt.MapFrom(src => src.LOAN_FEATURES))
                .ForMember(dest => dest.RateAdjustment, opt => opt.MapFrom(src => src.RATE_ADJUSTMENT));               
            });
        }

        public LoanProductData Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            LoanProductData destination = iMapper.Map<LoanProductDataDTO, LoanProductData>(_mismo231);
            return destination;
        }
    }
}
