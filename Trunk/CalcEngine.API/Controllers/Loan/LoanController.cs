using CalcEngine.API.App_Code;
using CalcEngine.API.App_Code.FileManager;
using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs;
using CalcEngine.Calcs.DocType;
using CalcEngine.Logging;
using CalcEngine.Models;
using CalcEngine.Models.Mismo;
using CalcEngine.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface;
using System;
using System.Threading.Tasks;

namespace CalcEngine.API.Controllers.Loan
{
    [Produces("application/json")]
    [Route("api/Loan")]
    public class LoanController : Controller
    {
        private IConfiguration _configuration = null;
        private IGetContentRequest _fileManagerApiClient = null;
        private ILoanManager _IUSLoanManager = null;

        public LoanController(
            IConfiguration configuration,
            ILoanManager IUSLoanManager,
            IGetContentRequest fileManagerApiClient)
        {            
            _configuration = configuration;
            _IUSLoanManager = IUSLoanManager;
            _fileManagerApiClient = fileManagerApiClient;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{fileid}/ProgramReservesRequired/")]
        public async Task<ActionResult> ProgramReservesRequired(string fileId)
        {
            try
            {
                this.Info("Calculating Program Reserves Required");
                // Validate custom header values
                CustomHeaderData customHeaderData = new CustomHeaderData(HttpContext.Request.Headers);
                customHeaderData.ValidateCustomHeaders();

                // Get the file from disk
                LoanCalcRequestModel loanCalcRequestModel =
                    await _fileManagerApiClient.GetLoanCalcRequestAsync(fileId, $"{_configuration["FileManagerApi:ApiUrl"]}");

                // Use custom header information and mismo content to create IUSLoan & DocTypeCalcResolver
                _IUSLoanManager.SetUp(customHeaderData, loanCalcRequestModel);

                DocTypeCalcResolver docTypeCalcResolver = _IUSLoanManager.GetDocTypeCalcResolver();
                IUSLoan iusLoan = _IUSLoanManager.GetIUSLoan();

                // Use Factory to create Calculations Interface
                Calculations calcs = DocTypeCalcFactory.Create(docTypeCalcResolver, iusLoan);

                decimal total = calcs.LoanCalcs.ProgramReservesRequired();
                return Ok(total);
            }
            catch (Exception ex)
            {
                this.Error("Error in Calculating Program Reserves Required", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{fileid}/AdditionalReservesRequired/")]
        public async Task<ActionResult> AdditionalReservesRequired(string fileId)
        {
            try
            {
                this.Info("Calculating Additional Reserves Required");
                // Validate custom header values
                CustomHeaderData customHeaderData = new CustomHeaderData(HttpContext.Request.Headers);
                customHeaderData.ValidateCustomHeaders();

                // Get the file from disk
                LoanCalcRequestModel loanCalcRequestModel =
                    await _fileManagerApiClient.GetLoanCalcRequestAsync(fileId, $"{_configuration["FileManagerApi:ApiUrl"]}");

                // Use custom header information and mismo content to create IUSLoan & DocTypeCalcResolver
                _IUSLoanManager.SetUp(customHeaderData, loanCalcRequestModel);

                DocTypeCalcResolver docTypeCalcResolver = _IUSLoanManager.GetDocTypeCalcResolver();
                IUSLoan iusLoan = _IUSLoanManager.GetIUSLoan();

                // Use Factory to create Calculations Interface
                Calculations calcs = DocTypeCalcFactory.Create(docTypeCalcResolver, iusLoan);

                decimal total = calcs.LoanCalcs.AdditionalReservesRequired();
                return Ok(total);
            }
            catch (Exception ex)
            {
                this.Error("Error in Calculating Additional Reserves Required", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
