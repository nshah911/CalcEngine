using AutoMapper;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class AdditionalCaseDataMapper
    {
        private AdditionalCaseDataDTO _mismo231 = null;
        private MapperConfiguration _config = null;

        public AdditionalCaseDataMapper(AdditionalCaseDataDTO mismo231)
        {
            _mismo231 = mismo231;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AdditionalCaseDataDTO, AdditionalCaseData>()
               .ForMember(dest => dest.TransmittalData, opt => opt.MapFrom(src => src.TRANSMITTAL_DATA));
            });
        }

        public AdditionalCaseData Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            AdditionalCaseData destination = iMapper.Map<AdditionalCaseDataDTO, AdditionalCaseData>(_mismo231);
            return destination;
        }
    }
}
