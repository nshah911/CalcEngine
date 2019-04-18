using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{
    public class CreditScore
    {
        public string _BorId { get; set; }
        public string _CreditFileId { get; set; }
        public string _CreditReportIdentifier { get; set; }
        public CreditRepositorySourceType _CreditRepositorySourceType { get; set; }
        public string _DateRaw { get; set; }
        public DateTime _Date
        {
            get
            {
                if (DateTime.TryParse(_DateRaw, out DateTime d))
                    return d;
                else
                    return DateTime.MinValue;
            }
        }
        public string _ValueRaw { get; set; }
        public int _Value
        {
            get
            {
                if (!String.IsNullOrEmpty(_ValueRaw))
                    return Int32.Parse(_ValueRaw.Replace("+", ""));
                else
                    return 0;
            }
        }

    }
    public enum CreditRepositorySourceType
    {

        /// <remarks/>
        Equifax,

        /// <remarks/>
        Experian,

        /// <remarks/>
        MergedData,

        /// <remarks/>
        Other,

        /// <remarks/>
        TransUnion,
    }
}
