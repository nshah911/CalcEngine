using CalcEngine.Models.Credit;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface.Credit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MismoCalcs.Implementation.Credit
{
    public class CreditModelCommonCalcs : ICreditCalcs, ICreditLiabilityCalcs
    {
        private readonly IEnumerable<CreditResponseGroup> _responseGroups;

        public CreditModelCommonCalcs(IEnumerable<CreditResponseGroup> responseGroups)
        {
            _responseGroups = responseGroups;
        }

        /// <summary>
        /// Lowest of all borrowers. For each borrower, lower of two, middle of three.
        /// </summary>
        /// <returns></returns>
        public int DecisionCreditScore()
        {
            int result = 0;
            List<string> Borrowers = new List<string>();
            List<CreditScore> scores = new List<CreditScore>();
            List<int> midCreditScores = new List<int>();

            foreach(CreditResponseGroup respGrp in _responseGroups)
            {
                Borrowers = respGrp.Response.ResponseData.CreditResponse.CreditScores.Select(x => x._BorId).Distinct().ToList();
                if(Borrowers != null)
                {
                    foreach(string BorId in Borrowers)
                    {
                        List<CreditScore> CreditScorePerBorr = respGrp.Response.ResponseData.CreditResponse.CreditScores.Where(t => t._BorId == BorId).ToList();
                        int mid = SelectBorrowerScore(CreditScorePerBorr);
                        midCreditScores.Add(mid);
                    }
                }
            }
            result = midCreditScores.Min();
            return result;
        }

        public int LatePaymentCount()
        {
            throw new NotImplementedException();
        }

        public int LatePaymentCount(AccountType accountType)
        {
            throw new NotImplementedException();
        }

        public int LatePaymentCount(AccountType accountType, PriorAdverseRatingType priorAdverseRatingType)
        {
            throw new NotImplementedException();
        }

        public int LatePaymentCount(AccountType accountType, PriorAdverseRatingType priorAdverseRatingType, int withinMonths)
        {
            int result = 0;
            List<CreditLiability> creditLiabilities = new List<CreditLiability>();
            foreach (CreditResponseGroup respGrp in _responseGroups)
            {
                DateTime FistDayOfResponseDate = new DateTime(respGrp.Response.ResponseDateTime.Year, respGrp.Response.ResponseDateTime.Month, 1);
                DateTime MnthspriorResponseDate = FistDayOfResponseDate.AddMonths((withinMonths - 1) * -1);

                creditLiabilities = GetLiabilityNotExcludeOrPayOffs(respGrp.Response.ResponseData.CreditResponse.CreditLiabilities, accountType);
                if (creditLiabilities.Count() > 0)
                {
                    foreach (CreditLiability cl in creditLiabilities)
                    {
                        int Count = cl._PriorAdverseRatings.Where(t => t._Type == priorAdverseRatingType 
                        && t._Date >= MnthspriorResponseDate && t._Date <= FistDayOfResponseDate).Count();
                        result += Count;
                    }
                }
            }
            return result;
        }

        public List<CreditLiability> GetLiabilityNotExcludeOrPayOffs(List<CreditLiability> creditLiabilities, AccountType accountType)
        {
            List<CreditLiability> creditLiabilitiesNotExcludeOrPayoffs = new List<CreditLiability>();
            if (creditLiabilities != null)
                creditLiabilitiesNotExcludeOrPayoffs = creditLiabilities
                    .Where(t => t.Liability != null
                    && t._AccountType == accountType
                    && ((t.Liability._ExclusionIndicator == CalcEngine.Models.Mismo.LiabilityExclusionIndicator.N && t.Liability._PayoffStatusIndicator == CalcEngine.Models.Mismo.LiabilityPayoffStatusIndicator.N)
                    || (t.Liability._ExclusionIndicator == CalcEngine.Models.Mismo.LiabilityExclusionIndicator.N && t.Liability._PayoffStatusIndicator == CalcEngine.Models.Mismo.LiabilityPayoffStatusIndicator.Unknown)
                    || (t.Liability._ExclusionIndicator == CalcEngine.Models.Mismo.LiabilityExclusionIndicator.Unknown && t.Liability._PayoffStatusIndicator == CalcEngine.Models.Mismo.LiabilityPayoffStatusIndicator.N)
                    || (t.Liability._ExclusionIndicator == CalcEngine.Models.Mismo.LiabilityExclusionIndicator.Unknown && t.Liability._PayoffStatusIndicator == CalcEngine.Models.Mismo.LiabilityPayoffStatusIndicator.Unknown)))
                    .ToList();
            return creditLiabilitiesNotExcludeOrPayoffs;
        }

        public int BankruptcyCount(int withinMonths)
        {
            int result = 0;
            foreach (CreditResponseGroup respGrp in _responseGroups)
            {
                DateTime MonthspriorResponseDate = respGrp.Response.ResponseDateTime.AddMonths((withinMonths - 1) * -1);

                result += respGrp.Response.ResponseData.CreditResponse.CreditPublicRecords.Where(t => 
                (t._Type == CreditPublicRecordType.BankruptcyTypeUnknown || t._Type == CreditPublicRecordType.BankruptcyChapter11 
                || t._Type == CreditPublicRecordType.BankruptcyChapter12 || t._Type == CreditPublicRecordType.BankruptcyChapter13 
                || t._Type == CreditPublicRecordType.BankruptcyChapter7 || t._Type == CreditPublicRecordType.BankruptcyChapter7Involuntary
                || t._Type == CreditPublicRecordType.BankruptcyChapter7Voluntary) && (t._FiledDate == DateTime.MinValue 
                || (t._FiledDate >= MonthspriorResponseDate && t._FiledDate <= respGrp.Response.ResponseDateTime))).Count();

            }
            return result;
        }

        public int ForeclosureCount(int withinMonths)
        {
            int result = 0;
            foreach (CreditResponseGroup respGrp in _responseGroups)
            {
                DateTime MonthspriorResponseDate = respGrp.Response.ResponseDateTime.AddMonths((withinMonths - 1) * -1);
                DateTime FirstDayMonthPriorRespDate = new DateTime(MonthspriorResponseDate.Year, MonthspriorResponseDate.Month, 1);
                DateTime FirstDayRespDate = new DateTime(respGrp.Response.ResponseDateTime.Year, respGrp.Response.ResponseDateTime.Month, 1);

                if (respGrp.Response.ResponseData.CreditResponse.CreditPublicRecords != null)
                {
                    result += respGrp.Response.ResponseData.CreditResponse.CreditPublicRecords.Where(t =>
                    (t._Type == CreditPublicRecordType.Foreclosure || t._Type == CreditPublicRecordType.NoticeOfDefault
                    || t._Type == CreditPublicRecordType.RealEstateRecording) && (t._DispositionDate == DateTime.MinValue
                    || (t._DispositionDate >= MonthspriorResponseDate && t._DispositionDate <= respGrp.Response.ResponseDateTime))).Count();
                }

                if (respGrp.Response.ResponseData.CreditResponse.CreditLiabilities != null)
                {
                    List<CreditLiability> creditLiabilities = respGrp.Response.ResponseData.CreditResponse.CreditLiabilities.Where(t =>
                    t._AccountType == AccountType.Mortgage || t._AccountType == AccountType.CreditLine).ToList();

                    foreach (CreditLiability cl in creditLiabilities)
                    {
                        if (FoundForeclosureInHighestAdverseRating(cl, FirstDayMonthPriorRespDate, respGrp.Response.ResponseDateTime))
                        {
                            result += 1;
                            continue;
                        }
                        if (FoundForeClosureInCurrentRating(cl, MonthspriorResponseDate, respGrp.Response.ResponseDateTime))
                        {
                            result += 1;
                            continue;
                        }
                        if (FoundForeclosureInMostRecentAdverseRating(cl, FirstDayMonthPriorRespDate, FirstDayRespDate))
                        {
                            result += 1;
                            continue;
                        }
                        if (FoundForeclosureInPriorAdverseRating(cl, FirstDayMonthPriorRespDate, FirstDayRespDate))
                        {
                            result += 1;
                            continue;
                        }

                    }
                }
            }
            return result;
        }

        private bool FoundForeclosureInPriorAdverseRating(CreditLiability cl, DateTime startDate, DateTime endDate)
        {
            bool foundForeClosure = false;
            int count = 0;
            if(cl._PriorAdverseRatings != null)
            {
                count = cl._PriorAdverseRatings.Where(t => (t._Type == PriorAdverseRatingType.Foreclosure || t._Type == PriorAdverseRatingType.ForeclosureOrRepossession)
                && (t._Date == DateTime.MinValue || (t._Date >= startDate && t._Date <= endDate))).Count();

                if (count > 0)
                    foundForeClosure = true;
            }

            return foundForeClosure;
        }

        private bool FoundForeclosureInMostRecentAdverseRating(CreditLiability cl, DateTime startDate, DateTime endDate)
        {
            bool foundForeclosure = false;
            if(cl._MostRecentAdverseRating != null)
            {
                if ((cl._MostRecentAdverseRating._Type == MostRecentAdverseRatingType.Foreclosure || cl._MostRecentAdverseRating._Type == MostRecentAdverseRatingType.ForeclosureOrRepossession)
                && (cl._MostRecentAdverseRating._Date == DateTime.MinValue
                    || (cl._MostRecentAdverseRating._Date >= startDate && cl._MostRecentAdverseRating._Date <= endDate)))
                {
                    foundForeclosure = true;
                }
            }
            return foundForeclosure;
        }

        private bool FoundForeClosureInCurrentRating(CreditLiability cl, DateTime startDate, DateTime endDate)
        {
            bool foundForeclosure = false;
            if(cl._CurrentRating != null)
            {
                if ((cl._CurrentRating._Type == LiabilityCurrentRatingType.Foreclosure || cl._CurrentRating._Type == LiabilityCurrentRatingType.ForeclosureOrRepossession)
                && (cl._AccountStatusDate == DateTime.MinValue
                    || (cl._AccountStatusDate >= startDate && cl._AccountStatusDate <= endDate)))
                {
                    foundForeclosure = true;
                }
            }
            
            return foundForeclosure;
        }

        private bool FoundForeclosureInHighestAdverseRating(CreditLiability cl, DateTime startDate, DateTime endDate)
        {
            bool foundForeclosure = false;
            if(cl._HighestAdverseRating != null)
            {
                if ((cl._HighestAdverseRating._Type == HighestAdverseRatingType.Foreclosure || cl._HighestAdverseRating._Type == HighestAdverseRatingType.ForeclosureOrRepossession)
                && (cl._HighestAdverseRating._Date == DateTime.MinValue
                    || (cl._HighestAdverseRating._Date >= startDate && cl._HighestAdverseRating._Date <= endDate)))
                {
                    foundForeclosure = true;
                }
            }
            return foundForeclosure;
        }

        private int SelectBorrowerScore(List<CreditScore> creditScorePerBorr)
        {
            int? borrScore = null;
            IQueryable<CreditScore> order = creditScorePerBorr.AsQueryable().OrderBy(t => t._Value);
            if (order.Count() > 0)
            {
                int middlix = ((order.Count() + 1) >> 1) - 1;

                borrScore = order.ElementAt(middlix)._Value;
            }
            return borrScore.HasValue ? borrScore.Value : 0;
        }
    }
}
