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
    public class MortgageTermsMapper
    {
        private MortgageTermsDTO _mismo231 = null;
        private MapperConfiguration _config = null;

        public MortgageTermsMapper(MortgageTermsDTO mismo231)
        {
            _mismo231 = mismo231;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MortgageTermsDTO, MortgageTerms>();
            });
        }

        public MortgageTerms Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            MortgageTerms destination = iMapper.Map<MortgageTermsDTO, MortgageTerms>(_mismo231);
            return destination;
        }
    }
}