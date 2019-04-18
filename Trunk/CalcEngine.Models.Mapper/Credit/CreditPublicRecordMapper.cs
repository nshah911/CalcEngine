using AutoMapper;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.Credit
{
    public class CreditPublicRecordMapper
    {
        private CREDITREPORTING_CREDIT_CREDIT_PUBLIC_RECORD_Type[] _publicRecord = null;
        private MapperConfiguration _config = null;

        public CreditPublicRecordMapper(CREDITREPORTING_CREDIT_CREDIT_PUBLIC_RECORD_Type[] publicRecord)
        {
            this._publicRecord = publicRecord;
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CREDITREPORTING_CREDIT_CREDIT_PUBLIC_RECORD_Type, CreditPublicRecord>();
            });
        }

        internal List<CreditPublicRecord> Convert()
        {
            IMapper iMapper = _config.CreateMapper();
            List<CreditPublicRecord> destination = iMapper.Map<CREDITREPORTING_CREDIT_CREDIT_PUBLIC_RECORD_Type[], List<CreditPublicRecord>>(_publicRecord);
            return destination;
        }
    }
}
