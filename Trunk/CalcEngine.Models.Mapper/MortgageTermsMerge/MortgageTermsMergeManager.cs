using CalcEngine.Models.Mismo;
using CalcEngine.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcEngine.Models.Mapper.MortgageTermsMerge
{
    public class MortgageTermsMergeManager
    {
        private readonly MortgageTermsInfo _source;

        public MortgageTermsMergeManager(MortgageTermsInfo source)
        {
            _source = source;
        }

        public void MergeWith(MortgageTerms mortgageTerms)
        {
            if (!String.IsNullOrEmpty(_source.QualifyingPI))
            {
                if (decimal.TryParse(_source.QualifyingPI, out decimal qualifyingPI))
                    mortgageTerms.QualifyingPIRate = qualifyingPI;
                else
                    mortgageTerms.QualifyingPIRate = decimal.MinValue;
            }
        }
    }
}
