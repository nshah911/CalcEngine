using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Mismo_v231;
using CalcEngine.Models.Mismo_v26;
using NLog;

namespace CalcEngine.Models.Mapper.Mismo
{
    public class ReoPropertyMapper
    {
        private ReoPropertyDTO _mismo231 = null;
        private List<Liability> liabilities = new List<Liability>();
        private MapperConfiguration _config = null;

        public ReoPropertyMapper(ReoPropertyDTO mismo231, List<Liability> mismoLiabilities)
        {
            _mismo231 = mismo231;
            liabilities = mismoLiabilities;

            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReoPropertyDTO, ReoProperty>();
            });
        }

        public ReoProperty Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            ReoProperty destination = iMapper.Map<ReoPropertyDTO, ReoProperty>(_mismo231);
            destination.LinkedLiabilities = GetLiabilitiesOfREOs(liabilities, destination);
            return destination;
        }

        private List<Liability> GetLiabilitiesOfREOs(List<Liability> liabilities, ReoProperty reoProperty)
        {
            try
            {
                List<Liability> LinkedLiabilities = new List<Liability>();
                string[] linkedLiabs = null;

                if (liabilities != null)
                {
                    if (!String.IsNullOrEmpty(reoProperty.LiabilityID))
                    {
                        linkedLiabs = reoProperty.LiabilityID.Trim().Split(' ');
                        foreach (string liabId in linkedLiabs)
                        {
                            Liability liability = liabilities.Where(t => t._ID.Trim().Equals(liabId)).FirstOrDefault();
                            if (liability != null)
                            {
                                LinkedLiabilities.Add(liability);
                            }
                        }
                    }
                }
                return LinkedLiabilities;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}