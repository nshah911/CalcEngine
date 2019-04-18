using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;

namespace CalcEngine.Models.Mapper.Credit
{
    public class CreditLiabilityMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_LIABILITY_Type _creditXml;
        private MapperConfiguration _config = null;

        public CreditLiabilityMapper(CREDITREPORTING_CREDIT_CREDIT_LIABILITY_Type creditXml)
        {
            _creditXml = creditXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_Type, CreditLiability>()
                .ForMember(dest => dest._HighestAdverseRating, opt => opt.Ignore())
                .ForMember(dest => dest._CurrentRating, opt => opt.Ignore())
                .ForMember(dest => dest._MostRecentAdverseRating, opt => opt.Ignore())
                .ForMember(dest => dest._CreditLimitAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._CreditLimitAmount) ? 0M : decimal.Parse(src._CreditLimitAmount)))
                .ForMember(dest => dest._BalloonPaymentAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._BalloonPaymentAmount) ? 0M : decimal.Parse(src._BalloonPaymentAmount)))
                .ForMember(dest => dest._ChargeOffAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._ChargeOffAmount) ? 0M : decimal.Parse(src._ChargeOffAmount)))
                .ForMember(dest => dest._UnpaidBalanceAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._UnpaidBalanceAmount) ? 0M : decimal.Parse(src._UnpaidBalanceAmount)))
                .ForMember(dest => dest._HighBalanceAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._HighBalanceAmount) ? 0M : decimal.Parse(src._HighBalanceAmount)))
                .ForMember(dest => dest._HighCreditAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._HighCreditAmount) ? 0M : decimal.Parse(src._HighCreditAmount)))
                .ForMember(dest => dest._PastDueAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._PastDueAmount) ? 0M : decimal.Parse(src._PastDueAmount)))
                .ForMember(dest => dest._MonthlyPaymentAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._MonthlyPaymentAmount) ? 0M : decimal.Parse(src._MonthlyPaymentAmount)))
                .ForMember(dest => dest._LADRaw, opt => opt.MapFrom(src => src._LastActivityDate))
                .ForMember(dest => dest._AODRaw, opt => opt.MapFrom(src => src._AccountOpenedDate));

                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PAYMENT_PATTERN_Type, PaymentPattern>()
                .ForMember(dest => dest._StartDateRaw, opt => opt.MapFrom(src => src._StartDate));
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_LATE_COUNT_Type, LateCount>();
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PRIOR_ADVERSE_RATING_Type, PriorAdverseRating>()
                .ForMember(dest => dest._DateRaw, opt => opt.MapFrom(src => src._Date));
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_HIGHEST_ADVERSE_RATING_Type, HighestAdverseRating>()
                .ForMember(dest => dest._DateRaw, opt => opt.MapFrom(src => src._Date));
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_CURRENT_RATING_Type, CurrentRating>();
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_MOST_RECENT_ADVERSE_RATING_Type, MostRecentAdverseRating>()
                .ForMember(dest => dest._Amount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src._Amount) ? 0M : decimal.Parse(src._Amount)))
                .ForMember(dest => dest._DateRaw, opt => opt.MapFrom(src => src._Date));


            });
        }

        internal CreditLiability Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            CreditLiability destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_Type, CreditLiability>(_creditXml);
            LateCount lateCount = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_LATE_COUNT_Type, LateCount>(_creditXml._LATE_COUNT);
            PaymentPattern paymentpattern = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PAYMENT_PATTERN_Type, PaymentPattern>(_creditXml._PAYMENT_PATTERN);
            List<PriorAdverseRating> listPriorAdverseRatings = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_PRIOR_ADVERSE_RATING_Type[], List<PriorAdverseRating>>(_creditXml._PRIOR_ADVERSE_RATING);
            HighestAdverseRating listHighestAdverseRating = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_HIGHEST_ADVERSE_RATING_Type, HighestAdverseRating>(_creditXml._HIGHEST_ADVERSE_RATING);
            CurrentRating currentRating = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_CURRENT_RATING_Type, CurrentRating>(_creditXml._CURRENT_RATING);
            MostRecentAdverseRating mostRecentAdverseRating = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_LIABILITY_MOST_RECENT_ADVERSE_RATING_Type, MostRecentAdverseRating>(_creditXml._MOST_RECENT_ADVERSE_RATING);
            destination._PaymentPattern = paymentpattern;
            destination._LateCount = lateCount;
            destination._PriorAdverseRatings = listPriorAdverseRatings;
            destination._HighestAdverseRating = listHighestAdverseRating;
            destination._CurrentRating = currentRating;
            destination._MostRecentAdverseRating = mostRecentAdverseRating;
            return destination;
        }
    }
}
