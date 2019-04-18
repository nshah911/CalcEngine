using CalcEngine.Models.Credit;
using CalcEngine.Models.Mapper.LiabilityMerge;
using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CalcEngine.Models.Mapper.Tests.LiabilityMerge
{
    public class LiabilityMergeManagerTests
    {
        [Fact]
        public void LiabilityMerge_has_no_match_on_acctId_lastActivityDt_should_add_new_liability()
        {
            // Arrange
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            List<Liability> liabilities = new List<Liability>();

            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "123", _LastActivityDate = DateTime.Parse("2018-06-15") });
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "456", _LastActivityDate = DateTime.Parse("2018-06-15") });
            liabilities.Add(new Liability { _ID = "L1", _AccountIdentifier = "xyz" });            
            // Act
            LiabilityMergeManager merge = new LiabilityMergeManager(creditLiabilities);
            merge.MergeWith(liabilities);

            // Assert
            Assert.Equal<int>(3, liabilities.Count);
        }

        [Fact]
        public void LiabilityMerge_found_match_on_account_id_should_add_new_liability()
        {
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            List<Liability> liabilities = new List<Liability>();

            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "123", _LastActivityDate = DateTime.Parse("2018-06-15") });
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "456", _LastActivityDate = DateTime.Parse("2018-06-15") });
            liabilities.Add(new Liability { _ID = "L1", _AccountIdentifier = "123" });
            LiabilityMergeManager merge = new LiabilityMergeManager(creditLiabilities);
            merge.MergeWith(liabilities);

            // Assert
            Assert.Equal<int>(2, liabilities.Count);
        }
        

        [Fact]
        public void LiabilityMerge_has_two_match_on_acctId_or_lastActivityDt_merge_two_liability()
        {
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            List<Liability> liabilities = new List<Liability>();

            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "101112", _LastActivityDate = DateTime.Parse("2018-06-15") });
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "101112", _LastActivityDate = DateTime.Parse("2018-06-15") });
            liabilities.Add(new Liability { _ID = "L4", _AccountIdentifier = "101112" });
            liabilities.Add(new Liability { _ID = "L5", _AccountIdentifier = "101112" });

            LiabilityMergeManager merge = new LiabilityMergeManager(creditLiabilities);
            merge.MergeWith(liabilities);

            // Assert
            Assert.Equal<int>(2, liabilities.Count);
        }


        /// <summary>
        /// Checking merge of all the scenario
        /// 1. 1 account in Credit liability and 1 in MISMO (Map one to one)
        /// 2. 2 same account in the credit liability and 1 in MISMO (map first and create new liab for other)
        /// 3. 2 same account in credit liability and 2 in MISMO (both map to MISMO)
        /// </summary>
        [Fact]
        public void LiabilityMerge_all_Scenario()
        {
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            List<Liability> liabilities = new List<Liability>();
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "123", _LastActivityDate = DateTime.Parse("2018-06-15") });
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "789", _LastActivityDate = DateTime.Parse("2018-06-15") });
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "789", _LastActivityDate = DateTime.Parse("2018-07-15") });
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "101112", _LastActivityDate = DateTime.Parse("2018-06-15") });
            creditLiabilities.Add(new CreditLiability { _AccountIdentifier = "101112", _LastActivityDate = DateTime.Parse("2018-07-15") });
            liabilities.Add(new Liability { _ID = "L1", _AccountIdentifier = "123" });
            liabilities.Add(new Liability { _ID = "L2", _AccountIdentifier = "456" });
            liabilities.Add(new Liability { _ID = "L3", _AccountIdentifier = "789" });
            liabilities.Add(new Liability { _ID = "L4", _AccountIdentifier = "101112" });
            liabilities.Add(new Liability { _ID = "L5", _AccountIdentifier = "101112" });

            // Act
            LiabilityMergeManager merge = new LiabilityMergeManager(creditLiabilities);
            merge.MergeWith(liabilities);

            // Assert
            Assert.Equal<int>(6, liabilities.Count);
        }

    }
}
