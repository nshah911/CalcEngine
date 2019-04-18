using CalcEngine.Models.Credit;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalcEngine.API.App_Code
{
    public interface IGetContentRequest
    {
        Task<MismoContentModel> GetMismoContentAsync(string fileId, string apiUrlBase);

        Task<List<CreditContentModel>> GetCreditContentAsync(string fileId, string apiUrlBase);

        Task<LoanCalcRequestModel> GetLoanCalcRequestAsync(string fileId, string apiUrlBase);
    }
}