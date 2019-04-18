using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{
    public class PaymentPattern
    {
        public string _Data { get; set; }
        public string _StartDateRaw { get; set; }
        public DateTime _StartDate
        {
            get
            {
                if (DateTime.TryParse(_StartDateRaw, out DateTime d))
                    return d;
                else
                    return DateTime.MinValue;
            }
        }
    }
}
