using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class ResidenceMapper
    {
        private CREDITREPORTING_CREDIT_BORROWER_RESIDENCE_Type _residenceXml;
        private MapperConfiguration _config = null;
        public ResidenceMapper(CREDITREPORTING_CREDIT_BORROWER_RESIDENCE_Type residenceXml)
        {
            _residenceXml = residenceXml;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_BORROWER_RESIDENCE_Type, Residence>();
            });
        }

        public Residence ConvertResidence()
        {
            IMapper iMapper = _config.CreateMapper();
            Residence destination = iMapper.Map<CREDITREPORTING_CREDIT_BORROWER_RESIDENCE_Type, Residence>(_residenceXml);
            return destination;
        }
    }
}
