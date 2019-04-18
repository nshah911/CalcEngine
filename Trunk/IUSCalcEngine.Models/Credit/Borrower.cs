using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Credit
{
    public class Borrower
    {
        public string _BorrowerID { get; set; }
        public string _FirstName { get; set; }
        public string _LastName { get; set; }
        public PrintPositionType _PrintPositionType { get; set; }
        public string _SSN { get; set; }
        public Residence _Residence { get; set; }
    }
    public enum PrintPositionType
    {

        /// <remarks/>
        Borrower,

        /// <remarks/>
        CoBorrower,
    }

    public partial class Residence
    {
        public string _StreetAddress { get; set; }
        public string _City { get; set; }
        public string _State { get; set; }
        public string _PostalCode { get; set; }
        public ResidencyType _ResidencyType { get; set; }

        public enum ResidencyType
        {

            /// <remarks/>
            Current,

            /// <remarks/>
            Prior,
        }
    }

}
