using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CalcEngine.Models.Mapper.LiabilityMerge
{
    public class LiabilityMergeManager
    {
        private readonly List<CreditLiability> _source;

        public LiabilityMergeManager(List<CreditLiability> source)
        {
            _source = source;
        }

        public void MergeWith(List<Liability> liabilities)
        {
            HashSet<string> CreditLiabAdded = new HashSet<string>();
            Regex re = new Regex("[;\\/:*?\"<>|&'-]");

            foreach (CreditLiability Credit in _source)
            {
                if (!string.IsNullOrEmpty(Credit._AccountIdentifier))
                {
                    bool foundLiability = false; //flag for the liability matches

                    string accountIdentifier = re.Replace(Credit._AccountIdentifier.Trim().Replace(" ", ""), "");
                    string lastActivity = re.Replace(Credit._LastActivityDate.Date.ToString().Trim().Replace(" ", ""), "");
                    string s = accountIdentifier + "," + lastActivity;   // concating Account and LAD to check if liability already added
                    if (CreditLiabAdded.Contains(s)) //check if the liability is already mapped
                    {
                        continue;
                    }
                    else
                    {
                        foreach (Liability liab in liabilities)
                        {
                            if (liab._AccountIdentifier != null)
                            {
                                string mismoLiabAccount = re.Replace(liab._AccountIdentifier.Trim().Replace(" ", ""), "");

                                if (accountIdentifier.Equals(mismoLiabAccount, StringComparison.CurrentCultureIgnoreCase)) //comparing the the Account identifier of credit and mismo
                                {
                                    if (liab.CreditLiability == null)
                                    {
                                        liab.CreditLiability = Credit;
                                        Credit.Liability = liab;
                                        CreditLiabAdded.Add(s); //if match found add to Hash set
                                        foundLiability = true; //flag true found the liability
                                        break;
                                    }
                                }
                            }
                        }
                        if (!foundLiability)
                        {
                            //if liability not found then add new mismo liability
                            Liability notFoundLiab = new Liability();
                            notFoundLiab = CreateNewLiability(Credit);
                            Credit.Liability = notFoundLiab;
                            liabilities.Add(notFoundLiab);
                        }
                    }
                }
                
            }
        }

        private Liability CreateNewLiability(CreditLiability credit) //Create new mismo liab
        {
            Liability liability = new Liability();
            liability._ID = Guid.NewGuid().ToString();
            liability._AccountIdentifier = credit._AccountIdentifier;
            liability._ExclusionIndicator = LiabilityExclusionIndicator.N;
            liability._PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N;
            liability.CreditLiability = credit;
            return liability;
        }
    }
}
