using System;
using CalcEngine.Models;
using CalcEngine.Models.Enums;

namespace CalcEngine.Calcs.DocType
{
    public class DocTypeCalcResolver
    {
        public DocTypeEnum DocType { get; set; }

        public DateTime GuideLineDt { get; set; }

        public SourceType Source { get; set; }
    }
}