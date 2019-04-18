using CalcEngine.Models.Configuration;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using MismoCalcs.Implementation.Assets;
using MismoCalcs.Implementation.Credit;
using MismoCalcs.Interface.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MismoCalcs.Implementation.Tests.Credit
{
    public class LatePaymentCountTests
    {
        [Fact]
        public void LatePaymentCount_12_months_before_start_date_should_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            List<PriorAdverseRating> priorAdverseRatings = new List<PriorAdverseRating>();
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();

            PriorAdverseRating priorAdverseRating = new PriorAdverseRating();
            priorAdverseRating._DateRaw = "9/2017";
            priorAdverseRating._Type = PriorAdverseRatingType.Late30Days;
            priorAdverseRatings.Add(priorAdverseRating);
            CreditLiability creditLiability = new CreditLiability();
            creditLiability._AccountType = AccountType.Mortgage;
            creditLiability._PriorAdverseRatings = priorAdverseRatings;
            creditLiability.Liability = new Liability();
            creditLiability.Liability._ExclusionIndicator = LiabilityExclusionIndicator.N;
            creditLiability.Liability._PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N;
            creditLiabilities.Add(creditLiability);
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;
            
            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late30Days,12);
            // Assert
            Assert.Equal<int>(1, result);
        }

        [Fact]
        public void LatePaymentCount_11_months_before_start_date_should_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            List<PriorAdverseRating> priorAdverseRatings = new List<PriorAdverseRating>();
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();

            PriorAdverseRating priorAdverseRating = new PriorAdverseRating();
            priorAdverseRating._DateRaw = "10/2017";
            priorAdverseRating._Type = PriorAdverseRatingType.Late30Days;
            priorAdverseRatings.Add(priorAdverseRating);
            CreditLiability creditLiability = new CreditLiability();
            creditLiability._AccountType = AccountType.Mortgage;
            creditLiability._PriorAdverseRatings = priorAdverseRatings;
            creditLiability.Liability = new Liability();
            creditLiability.Liability._ExclusionIndicator = LiabilityExclusionIndicator.N;
            creditLiability.Liability._PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N;
            creditLiabilities.Add(creditLiability);
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late30Days, 12);
            // Assert
            Assert.Equal<int>(1, result);
        }

        [Fact]
        public void LatePaymentCount_13_months_before_start_date_should_not_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            List<PriorAdverseRating> priorAdverseRatings = new List<PriorAdverseRating>();
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();

            PriorAdverseRating priorAdverseRating = new PriorAdverseRating();
            priorAdverseRating._DateRaw = "8/2017";
            priorAdverseRating._Type = PriorAdverseRatingType.Late30Days;
            priorAdverseRatings.Add(priorAdverseRating);
            CreditLiability creditLiability = new CreditLiability();
            creditLiability._AccountType = AccountType.Mortgage;
            creditLiability._PriorAdverseRatings = priorAdverseRatings;
            creditLiability.Liability = new Liability();
            creditLiability.Liability._ExclusionIndicator = LiabilityExclusionIndicator.N;
            creditLiability.Liability._PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N;
            creditLiabilities.Add(creditLiability);
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late30Days, 12);
            // Assert
            Assert.Equal<int>(0, result);
        }

        [Fact]
        public void LatePaymentCount_for_30_daylate_within_12_months_should_be_2()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            List<PriorAdverseRating> priorAdverseRatings = new List<PriorAdverseRating>();
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            PriorAdverseRating priorAdverseRating2 = new PriorAdverseRating();
            PriorAdverseRating priorAdverseRating1 = new PriorAdverseRating();
            priorAdverseRating1._DateRaw = "10/2017";
            priorAdverseRating1._Type = PriorAdverseRatingType.Late30Days;
            priorAdverseRating2._DateRaw = "11/2017";
            priorAdverseRating2._Type = PriorAdverseRatingType.Late30Days;
            priorAdverseRatings.Add(priorAdverseRating1);
            priorAdverseRatings.Add(priorAdverseRating2);
            CreditLiability creditLiability = new CreditLiability();
            creditLiability._AccountType = AccountType.Mortgage;
            creditLiability._PriorAdverseRatings = priorAdverseRatings;
            creditLiability.Liability = new Liability();
            creditLiability.Liability._ExclusionIndicator = LiabilityExclusionIndicator.N;
            creditLiability.Liability._PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N;
            creditLiabilities.Add(creditLiability);
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late30Days, 12);
            // Assert
            Assert.Equal<int>(2, result);
        }

        [Fact]
        public void LatePaymentCount_for_60_daylate_within_12_months_should_be_1()
        {
            int result = 0;
            string contents = string.Empty; ;
            foreach (string file in Directory.EnumerateFiles("C:\\Late\\Late.xml"))
            {
                 contents = File.ReadAllText(file);
            }
            JsonConvert.DeserializeObject(contents);


            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            List<PriorAdverseRating> priorAdverseRatings = new List<PriorAdverseRating>();
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();
            PriorAdverseRating priorAdverseRating1 = new PriorAdverseRating();
            priorAdverseRating1._DateRaw = "10/2017";
            priorAdverseRating1._Type = PriorAdverseRatingType.Late60Days;
            priorAdverseRatings.Add(priorAdverseRating1);
            CreditLiability creditLiability = new CreditLiability();
            creditLiability._AccountType = AccountType.Mortgage;
            creditLiability._PriorAdverseRatings = priorAdverseRatings;
            creditLiability.Liability = new Liability();
            creditLiability.Liability._ExclusionIndicator = LiabilityExclusionIndicator.N;
            creditLiability.Liability._PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N;
            creditLiabilities.Add(creditLiability);
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late60Days, 12);
            // Assert
            Assert.Equal<int>(1, result);
        }

        [Fact]
        public void LatePaymentCount_for_excluded_liabilities_should_not_be_counted()
        {
            int result = 0;
            // Arrange
            List<CreditResponseGroup> creditResponseGroups = new List<CreditResponseGroup>();
            List<PriorAdverseRating> priorAdverseRatings = new List<PriorAdverseRating>();
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            Response response = new Response();
            response.ResponseDateTime = DateTime.Parse("8/14/2018");
            response.ResponseData = new ResponseData();
            response.ResponseData.CreditResponse = new CreditResponse();

            PriorAdverseRating priorAdverseRating = new PriorAdverseRating();
            priorAdverseRating._DateRaw = "9/2017";
            priorAdverseRating._Type = PriorAdverseRatingType.Late30Days;
            priorAdverseRatings.Add(priorAdverseRating);
            CreditLiability creditLiability = new CreditLiability();
            creditLiability._AccountType = AccountType.Mortgage;
            creditLiability._PriorAdverseRatings = priorAdverseRatings;
            creditLiability.Liability = new Liability();
            creditLiability.Liability._ExclusionIndicator = LiabilityExclusionIndicator.Y;
            creditLiability.Liability._PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N;
            creditLiabilities.Add(creditLiability);
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            creditResponseGroup.Response = response;

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditLiabilities = creditLiabilities;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.LatePaymentCount(AccountType.Mortgage, PriorAdverseRatingType.Late30Days, 12);
            // Assert
            Assert.Equal<int>(0, result);
        }
    }
}
