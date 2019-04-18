using CalcEngine.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.DocType.Calcs.DocType
{
    public class DocTypeCalcResolver
    {
            public DocTypeEnum DocType { get; set; }

            public DateTime GuideLineDt { get; set; }

            public SourceType Source { get; set; }
    }
}
