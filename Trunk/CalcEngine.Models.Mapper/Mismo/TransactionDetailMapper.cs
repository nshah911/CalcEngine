using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;
using CalcEngine.Models.Mismo_v26;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class TransactionDetailMapper
    {
        private TransactionDetailDTO _mismo231 = null;
        private MapperConfiguration _config = null;

        public TransactionDetailMapper(TransactionDetailDTO mismo231)
        {
            _mismo231 = mismo231;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransactionDetailDTO, TransactionDetail>()
                    .ForMember(dest => dest.PurchaseCredits, opt => opt.MapFrom(src => src.PURCHASE_CREDIT));
            });
        }

        public TransactionDetail Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            TransactionDetail destination = iMapper.Map<TransactionDetailDTO, TransactionDetail>(_mismo231);
            return destination;
        }
    }
}