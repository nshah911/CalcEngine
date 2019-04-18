using CalcEngine.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalcEngine.API.App_Code.Helper
{
    public static class SourceTypeEval
    {
        public static SourceType Get(string source)
        {
            SourceType result = SourceType.Encompass;
            source = source.ToLower().Trim();

            switch (source)
            {
                case "encompass":
                    result = SourceType.Encompass;
                    break;
                case "cashcall":
                    result = SourceType.CashCall;
                    break;
                case "spectrum":
                    result = SourceType.Spectrum;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
