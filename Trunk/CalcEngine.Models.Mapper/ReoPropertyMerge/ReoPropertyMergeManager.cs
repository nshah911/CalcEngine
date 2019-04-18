using CalcEngine.Models.Mismo;
using CalcEngine.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.ReoPropertyMerge
{
    public class ReoPropertyMergeManager
    {
        private readonly List<ReoPropertyInfo> _source;

        public ReoPropertyMergeManager(List<ReoPropertyInfo> source)
        {
            _source = source;
        }

        public void MergeWith(List<ReoProperty> reoProperties)
        {
            foreach (ReoProperty reo in reoProperties)
            {
                if (reo.Reo_Id != null)
                {
                    // Find REO in _source with matching Reo_Id
                    ReoPropertyInfo reoSource = _source.Find(r => r.ReoId != null 
                                                                && r.ReoId.Trim() == reo.Reo_Id.Trim());

                    if (reoSource != null)
                    {
                        if (!String.IsNullOrEmpty(reoSource.PercentOfParticipation))
                        {
                            if (decimal.TryParse(reoSource.PercentOfParticipation, out decimal percentOfParticipation))
                                reo.PercentOfParticipation = percentOfParticipation;
                            else
                                reo.PercentOfParticipation = decimal.MinValue;
                        }

                        if (!String.IsNullOrEmpty(reoSource.PercentOfRental))
                        {
                            if (decimal.TryParse(reoSource.PercentOfRental, out decimal percentOfRental))
                                reo.PercentOfRental = percentOfRental;
                            else
                                reo.PercentOfRental = decimal.MinValue;
                        }

                        if (!String.IsNullOrEmpty(reoSource.AcquireDate))
                        {
                            if (DateTime.TryParse(reoSource.AcquireDate, out DateTime acquireDate))
                                reo.AcquireDate = acquireDate;
                            else
                                reo.AcquireDate = DateTime.MinValue;
                        }
                    }
                }
            }
        }
    }


}
