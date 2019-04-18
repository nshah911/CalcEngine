using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mismo
{
    public class Declaration
    {
        public YesOrNo IntentToOccupyType { get; set; }
    }
    public enum YesOrNo
    {
        Yes,
        No
    }
}

