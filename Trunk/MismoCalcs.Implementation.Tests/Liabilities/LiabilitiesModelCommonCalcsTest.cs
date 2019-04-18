using CalcEngine.Models.Mismo;
using MismoCalcs.Implementation;
using MismoCalcs.Implementation.Liabilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MismoCalcs.Implementation.Tests.Liabilities
{
    public class LiabilitiesModelCommonCalcsTest
    {
        [Fact]
        public void Total_Liabilities_Alimony_should_be_100()
        {
            // arrange
            List<Liability> liabilities = new List<Liability>();
            liabilities.Add(new Liability { _Type = LiabilityType.Alimony, _MonthlyPaymentAmount= 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.Alimony, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.JobRelatedExpenses, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator =LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.Y, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.Y });

            LiabilityModelCommonCalcs calc = new LiabilityModelCommonCalcs(liabilities);

            // act
            decimal actual = calc.Total(LiabilityType.Alimony);

            // assert
            decimal expected = 100.00m;
            Assert.Equal<decimal>(expected, actual);
        }
        [Fact]
        public void Total_Liabilities_Job_Related_should_be_50()
        {
            // arrange
            List<Liability> liabilities = new List<Liability>();
            liabilities.Add(new Liability { _Type = LiabilityType.Alimony, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.Alimony, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.JobRelatedExpenses, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.Y, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.Y });

            LiabilityModelCommonCalcs calc = new LiabilityModelCommonCalcs(liabilities);

            // act
            decimal actual = calc.Total(LiabilityType.JobRelatedExpenses);

            // assert
            decimal expected = 50.00m;
            Assert.Equal<decimal>(expected, actual);
        }

        [Fact]
        public void Total_Liabilities_ChildCare_should_be_100()
        {
            // arrange
            List<Liability> liabilities = new List<Liability>();
            liabilities.Add(new Liability { _Type = LiabilityType.Alimony, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.Alimony, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.JobRelatedExpenses, _MonthlyPaymentAmount = 50.00m });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.Y, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.N });
            liabilities.Add(new Liability { _Type = LiabilityType.ChildCare, _MonthlyPaymentAmount = 50.00m, _ExclusionIndicator = LiabilityExclusionIndicator.N, _PayoffStatusIndicator = LiabilityPayoffStatusIndicator.Y });

            LiabilityModelCommonCalcs calc = new LiabilityModelCommonCalcs(liabilities);

            // act
            decimal actual = calc.Total(LiabilityType.ChildCare, LiabilityPayoffStatusIndicator.N, LiabilityExclusionIndicator.N);

            // assert
            decimal expected = 100.00m;
            Assert.Equal<decimal>(expected, actual);
        }
    }
}
