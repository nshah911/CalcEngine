using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class BorrowerMapper
    {
        private List<BorrowerDTO> _mismo231 = null;
        private MapperConfiguration _config = null;

        public BorrowerMapper(List<BorrowerDTO> mismo231)
        {
            _mismo231 = mismo231;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BorrowerDTO, Borrower>()
                .ForMember(dest => dest.PresentHousingExpenses, opt => opt.MapFrom(src => src.PRESENT_HOUSING_EXPENSE))
                .ForMember(dest => dest._Incomes, opt => opt.MapFrom(src => src.CURRENT_INCOME));
                //.ForMember(dest => dest._Declaration , opt => opt.MapFrom(src => src.DECLARATION));
            });
        }

        public List<Borrower> Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            List<Borrower> destination = iMapper.Map<List<BorrowerDTO>, List<Borrower>>(_mismo231);
            return destination;
        }
    }
}