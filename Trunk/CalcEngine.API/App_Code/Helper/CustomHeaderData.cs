using CalcEngine.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalcEngine.API.App_Code.Helper
{
    public class CustomHeaderData
    {
        public DocTypeEnum DocTypeEnum { get; set; }
        public SourceType SourceType { get; set; }
        public DateTime GuidelineDt { get; set; }
        public string ImpacProgramCode { get; set; }

        private readonly IHeaderDictionary _headers;

        public CustomHeaderData(IHeaderDictionary headers)
        {
            _headers = headers;
        }

        public void ValidateCustomHeaders()
        {
            string guidelineDt = string.Empty;
            string source = string.Empty;
            string impacPrgCd = string.Empty;
            string docType = string.Empty;

            try
            {
                // Get custom headers from Request headers
                guidelineDt = ReadRequestHeader.GetHttpContextHeader(_headers, "x-guidelinedt");
                source = ReadRequestHeader.GetHttpContextHeader(_headers, "x-clientsource");
                impacPrgCd = ReadRequestHeader.GetHttpContextHeader(_headers, "x-impacprogramcode");
                docType = ReadRequestHeader.GetHttpContextHeader(_headers, "x-docType");
            }
            catch
            {
                throw new Exception("Could not read custom headers from request.");
            }

            // Convert string custom headers to types
            GuidelineDt = GetGuildelineDate(guidelineDt);
            SourceType = GetSourceType(source);
            ImpacProgramCode = impacPrgCd;
            DocTypeEnum = GetDocType(docType);
        }

        private DocTypeEnum GetDocType(string docType)
        {
            if (String.IsNullOrEmpty(docType))
                throw new Exception("The x-doctype custom header cannot be null or empty.");

            DocTypeEnum DocType = DocTypeEval.Get(docType);
            if(DocType == DocTypeEnum.Unknown)
            {
                throw new Exception("Could not map x-doxtype to allowed doc type values");
            }
            else
            {
                return DocType;
            }
        }

        private DateTime GetGuildelineDate(string guidelineDt)
        {
            if (String.IsNullOrEmpty(guidelineDt))
                throw new Exception("The x-guidelinedt custom header cannot be null or empty.");

            if (DateTime.TryParse(guidelineDt, out DateTime result))
                return result;
            else
                throw new Exception("Could not convert custom header x-guidelinedt to a date value.");
        }

        private SourceType GetSourceType(string source)
        {
            if (String.IsNullOrEmpty(source))
                throw new Exception("The x-clientsource custom header cannot be null or empty.");

            return SourceTypeEval.Get(source);
        }
    }
}
