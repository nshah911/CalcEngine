using CalcEngine.API.App_Code;
using CalcEngine.API.App_Code.FileManager;
using CalcEngine.API.App_Code.Helper;
using CalcEngine.Calcs;
using CalcEngine.Calcs.DocType;
using CalcEngine.Logging;
using CalcEngine.Models;
using CalcEngine.Models.Enums;
using CalcEngine.Models.Mismo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MismoCalcs.Interface;
using System;
using System.Threading.Tasks;

namespace CalcEngine.API.Controllers.Mismo
{
    [Produces("application/json")]
    [Route("api/mismo/ReoProperty")]
    public class ReoPropertyController : Controller
    { 
        private IConfiguration _configuration = null;
        private IGetContentRequest _fileManagerApiClient = null;
        private ILoanManager _IUSLoanManager = null;

        public ReoPropertyController(
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
        [Route("{fileid}/ReoPropertiesData")]
        public async Task<ActionResult> ReoPropertiesDataAsync(string fileId)
        {
            try
            {
                this.Info("Calculating in Reo Properties");
                // Validate custom header values
                CustomHeaderData customHeaderData = new CustomHeaderData(HttpContext.Request.Headers);
                customHeaderData.ValidateCustomHeaders();

                // Get the file from disk
                MismoContentModel mismoContentModel =
                    await _fileManagerApiClient.GetMismoContentAsync(fileId, $"{_configuration["FileManagerApi:ApiUrl"]}");

                // Use custom header information and mismo content to create IUSLoan & DocTypeCalcResolver
                _IUSLoanManager.SetUp(customHeaderData, mismoContentModel);

                DocTypeCalcResolver docTypeCalcResolver = _IUSLoanManager.GetDocTypeCalcResolver();
                IUSLoan iusLoan = _IUSLoanManager.GetIUSLoan();

                // Use Factory to create Calculations Interface
                Calculations calcs = DocTypeCalcFactory.Create(docTypeCalcResolver, iusLoan);

                if (iusLoan.MismoLoan.ReoProperties != null)
                    return Ok(iusLoan.MismoLoan.ReoProperties);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                this.Error("Error in calculating Reo Properties", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}