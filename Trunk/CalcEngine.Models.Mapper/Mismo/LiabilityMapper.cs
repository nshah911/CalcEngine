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
    public class LiabiltyMapper
    {
        private LiabilityDTO _mismo26 = null;
        private MapperConfiguration _config = null;

        public LiabiltyMapper(LiabilityDTO mismo26)
        {
            _mismo26 = mismo26;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LiabilityDTO, Liability>();
            });
        }

        public Liability Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            Liability destination = iMapper.Map<LiabilityDTO, Liability>(_mismo26);
            return destination;
        }
    }
}