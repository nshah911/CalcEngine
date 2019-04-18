using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;

namespace CalcEngine.Models.Mapper.Credit
{
    public class CreditScoreMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_SCORE_Type[] creditScore;
        private MapperConfiguration _config = null;

        public CreditScoreMapper(CREDITREPORTING_CREDIT_CREDIT_SCORE_Type[] creditScore)
        {
            this.creditScore = creditScore;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_SCORE_Type, CreditScore>()
                .ForMember(dest => dest._BorId, opt => opt.MapFrom(src => src.BorrowerID))
                .ForMember(dest => dest._DateRaw, opt => opt.MapFrom(src => src._Date))
                .ForMember(dest => dest._ValueRaw, opt => opt.MapFrom(src => src._Value));
            });
        }

        internal List<CreditScore> Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            List<CreditScore> destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_SCORE_Type[], List<CreditScore>>(this.creditScore);
            return destination;
        }
    }
}
