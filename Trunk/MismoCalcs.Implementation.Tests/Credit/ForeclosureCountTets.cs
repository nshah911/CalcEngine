using CalcEngine.Models.Configuration;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using MismoCalcs.Implementation.Assets;
using MismoCalcs.Implementation.Credit;
using MismoCalcs.Interface.Assets;
using System;
using System.Collections.Generic;
using Xunit;

namespace MismoCalcs.Implementation.Tests.Credit
{
    public class ForeclosureCountTets
    {
        [Fact]
        public void ForeclosureCount_public_record_within_120_months_should_be_counted()
        {
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditPublicRecord> creditPublicRecords = new List<CreditPublicRecord>();
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Foreclosure, _DispositionDate = DateTime.Parse("8/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.NoticeOfDefault, _DispositionDate = DateTime.Parse("8/15/2008") });
            
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            creditResponseGroups.Add(creditResponseGroup);

            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            int result = 0;
            result = creditModelCommonCalcs.ForeclosureCount(120);

            // Assert
            Assert.Equal<int>(2, result);
        }

        [Fact]
        public void ForeclosureCount_public_record_within_121_months_should_not_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditPublicRecord> creditPublicRecords = new List<CreditPublicRecord>();
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Foreclosure, _DispositionDate = DateTime.Parse("7/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.NoticeOfDefault, _DispositionDate = DateTime.Parse("7/15/2008") });

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.ForeclosureCount(120);
            // Assert
            Assert.Equal<int>(0, result);
        }

        [Fact]
        public void ForeclosureCount_public_record_within_119_months_should_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditPublicRecord> creditPublicRecords = new List<CreditPublicRecord>();
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Foreclosure, _DispositionDate = DateTime.Parse("10/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.NoticeOfDefault, _DispositionDate = DateTime.Parse("10/15/2008") });

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.ForeclosureCount(120);
            // Assert
            Assert.Equal<int>(2, result);
        }

        [Fact]
        public void ForeclosureCount_credit_liabiliy_mortgage_acct_check_for_highestAdverseRating_within_120_months_should_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditPublicRecord> creditPublicRecords = new List<CreditPublicRecord>();
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Foreclosure, _DispositionDate = DateTime.Parse("10/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.NoticeOfDefault, _DispositionDate = DateTime.Parse("10/15/2008") });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            creditLiabilities.Add(new CreditLiability() { _AccountType = AccountType.CreditLine, _HighestAdverseRating = new HighestAdverseRating() { _Type = HighestAdverseRatingType.Foreclosure, _DateRaw = "9/2008" }, _AccountStatusDate = DateTime.Parse("9/1/2010"), _CurrentRating = new CurrentRating() { _Type = LiabilityCurrentRatingType.Foreclosure } });
            creditLiabilities.Add(new CreditLiability() { _AccountType = AccountType.CreditLine, _HighestAdverseRating = new HighestAdverseRating() { _Type = HighestAdverseRatingType.ForeclosureOrRepossession, _DateRaw = "8/2008" } });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;

            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.ForeclosureCount(120);
            // Assert
            Assert.Equal<int>(3, result);
        }

        [Fact]
        public void ForeclosureCount_credit_liabiliy_mortgage_acct_check_for_currentRating_within_120_months_should_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditPublicRecord> creditPublicRecords = new List<CreditPublicRecord>();
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Foreclosure, _DispositionDate = DateTime.Parse("10/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.NoticeOfDefault, _DispositionDate = DateTime.Parse("10/15/2008") });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;

            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            creditLiabilities.Add(new CreditLiability() {_AccountType = AccountType.CreditLine, _AccountStatusDate = DateTime.Parse("9/1/2010"), _CurrentRating = new CurrentRating() { _Type = LiabilityCurrentRatingType.Foreclosure } });
            creditLiabilities.Add(new CreditLiability() { _HighestAdverseRating = new HighestAdverseRating() { _Type = HighestAdverseRatingType.ForeclosureOrRepossession, _DateRaw = "8/2008" } });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;

            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.ForeclosureCount(120);
            // Assert
            Assert.Equal<int>(3, result);
        }

        [Fact]
        public void ForeclosureCount_credit_liabiliy_mortgage_acct_check_for_mostRecentRating_within_120_months_should_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditPublicRecord> creditPublicRecords = new List<CreditPublicRecord>();
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Foreclosure, _DispositionDate = DateTime.Parse("10/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.NoticeOfDefault, _DispositionDate = DateTime.Parse("10/15/2008") });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;

            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            creditLiabilities.Add(new CreditLiability() { _AccountType = AccountType.CreditLine, _MostRecentAdverseRating = new MostRecentAdverseRating() { _Type = MostRecentAdverseRatingType.ForeclosureOrRepossession, _DateRaw = "9/2008" } });
            creditLiabilities.Add(new CreditLiability() { _AccountType = AccountType.Mortgage, _MostRecentAdverseRating = new MostRecentAdverseRating() { _Type = MostRecentAdverseRatingType.ForeclosureOrRepossession, _DateRaw = "9/2008" } });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;

            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.ForeclosureCount(120);
            // Assert
            Assert.Equal<int>(4, result);
        }

        [Fact]
        public void ForeclosureCount_credit_liabiliy_mortgage_acct_check_for_priorAdverseRating_within_120_months_should_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditPublicRecord> creditPublicRecords = new List<CreditPublicRecord>();
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Foreclosure, _DispositionDate = DateTime.Parse("10/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.NoticeOfDefault, _DispositionDate = DateTime.Parse("10/15/2008") });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;

            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            creditLiabilities.Add(new CreditLiability() { _AccountType = AccountType.CreditLine, _PriorAdverseRatings = new List<PriorAdverseRating>() { new PriorAdverseRating() { _Type = PriorAdverseRatingType.Foreclosure, _DateRaw = "9/2008" } } });
            creditLiabilities.Add(new CreditLiability() { _AccountType = AccountType.Mortgage, _PriorAdverseRatings = new List<PriorAdverseRating>() { new PriorAdverseRating() { _Type = PriorAdverseRatingType.ForeclosureOrRepossession, _DateRaw = "10/2008" } } });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;

            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.ForeclosureCount(120);
            // Assert
            Assert.Equal<int>(4, result);
        }

        [Fact]
        public void ForeclosureCount_credit_liabiliy_non_mortgage_heloc_acct__should_not_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            creditLiabilities.Add(new CreditLiability() { _HighestAdverseRating = new HighestAdverseRating() { _Type = HighestAdverseRatingType.ForeclosureOrRepossession, _DateRaw = "9/2008" }, _PriorAdverseRatings = new List<PriorAdverseRating>() { new PriorAdverseRating() { _Type = PriorAdverseRatingType.Foreclosure, _DateRaw = "9/2008" } } });
            creditLiabilities.Add(new CreditLiability() { _PriorAdverseRatings = new List<PriorAdverseRating>() { new PriorAdverseRating() { _Type = PriorAdverseRatingType.Foreclosure, _DateRaw = "9/2008" } } });
            creditLiabilities.Add(new CreditLiability() {  _PriorAdverseRatings = new List<PriorAdverseRating>() { new PriorAdverseRating() { _Type = PriorAdverseRatingType.ForeclosureOrRepossession, _DateRaw = "1/2008" } } });
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;

            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.ForeclosureCount(120);
            // Assert
            Assert.Equal<int>(0, result);
        }
    }
}
