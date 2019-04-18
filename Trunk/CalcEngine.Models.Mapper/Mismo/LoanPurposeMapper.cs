using AutoMapper;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class LoanPurposeMapper
    {
        private LoanPurposeDTO _mismo26 = null;
        private MapperConfiguration _config = null;

        public LoanPurposeMapper(LoanPurposeDTO mismo26)
        {
            _mismo26 = mismo26;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoanPurposeDTO, LoanPurpose>();
            });
        }

        public LoanPurpose Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            LoanPurpose destination = iMapper.Map<LoanPurposeDTO, LoanPurpose>(_mismo26);
            return destination;
        }
    }
}
