using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class AssetMapper
    {
        private AssetDTO _mismo231 = null;
        private MapperConfiguration _config = null;

        public AssetMapper(AssetDTO mismo231)
        {
            _mismo231 = mismo231;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AssetDTO, Asset>();
            });
        }

        public Asset Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            Asset destination = iMapper.Map<AssetDTO, Asset>(_mismo231);
            return destination;
        }
    }
}