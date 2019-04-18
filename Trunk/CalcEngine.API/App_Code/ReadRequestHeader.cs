using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalcEngine.API.App_Code
{
    public static class ReadRequestHeader
    {
        public static string GetHttpContextHeader(IHeaderDictionary headers, string key)
        {
            if (headers.TryGetValue(key, out StringValues result))
                return result;
            else
                return string.Empty;
        }
    }
}
