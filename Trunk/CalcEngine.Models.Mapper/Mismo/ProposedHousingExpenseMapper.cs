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
    public class ProposedHousingExpenseMapper
    {
        private HousingExpenseDTO _mismo231 = null;
        private MapperConfiguration _config = null;

        public ProposedHousingExpenseMapper(HousingExpenseDTO mismo231)
        {
            _mismo231 = mismo231;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HousingExpenseDTO, ProposedHousingExpense>();
            });
        }

        public ProposedHousingExpense Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            ProposedHousingExpense destination = iMapper.Map<HousingExpenseDTO, ProposedHousingExpense>(_mismo231);
            return destination;
        }
    }
}