using CalcEngine.Logging;
using CalcEngine.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FileManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class FileContentController : Controller
    {
        IConfiguration _configuration = null;

        public FileContentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("read/{fileid}")]
        public async Task<ActionResult> ReadAsync(string fileId)
        {
            try
            {
                LoanCalcRequestModel loanCalcModel = new LoanCalcRequestModel();

                // find the file with the matching fileid from disk
                string conn = $"{_configuration["FileStore:Connection"]}" + "\\" + fileId.ToString() + ".json";
                this.Info("Looking for fieldId" + fileId);
                if (!System.IO.File.Exists(conn))
                {
                    this.Info("The file id" + fileId + " was not found");
                    return NotFound("The file id " + fileId + " was not found.");
                }
                else
                {
                    this.Info("Reading all data from file");
                    string text = await System.IO.File.ReadAllTextAsync(conn);
                    loanCalcModel = JsonConvert.DeserializeObject<LoanCalcRequestModel>(text);
                }
                this.Info("Returning deserialized object");
                return Ok(loanCalcModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("read/mismocontent/{fileid}")]
        public async Task<ActionResult> ReadMismoContent(string fileId)
        {
            try
            {
                LoanCalcRequestModel loanCalcModel = new LoanCalcRequestModel();

                // find the file with the matching fileid from disk
                string conn = $"{_configuration["FileStore:Connection"]}" + "\\" + fileId.ToString() + ".json";
                this.Info("Looking for fileId " + fileId);
                if (!System.IO.File.Exists(conn))
                {
                    this.Info("The file id " + fileId + " was not found at connection: " + conn);
                    return NotFound("The file id " + fileId + " was not found.");
                }
                else
                {
                    this.Info("Reading all data from file.");
                    string text = await System.IO.File.ReadAllTextAsync(conn);
                    loanCalcModel = JsonConvert.DeserializeObject<LoanCalcRequestModel>(text);
                }
                this.Info("Returning deserializeed object.");
                return Ok(loanCalcModel.MismoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("read/creditcontent/{fileid}")]
        public async Task<ActionResult> ReadCreditContent(string fileId)
        {
            try
            {
                LoanCalcRequestModel loanCalcModel = new LoanCalcRequestModel();

                // find the file with the matching fileid from disk
                string conn = $"{_configuration["FileStore:Connection"]}" + "\\" + fileId.ToString() + ".json";
                this.Info("Looking for fileId " + fileId);
                if (!System.IO.File.Exists(conn))
                {
                    this.Info("The file id " + fileId + " was not found at connection: " + conn);
                    return NotFound("The file id " + fileId + " was not found.");
                }
                else
                {
                    this.Info("Reading all data from file.");
                    string text = await System.IO.File.ReadAllTextAsync(conn);
                    loanCalcModel = JsonConvert.DeserializeObject<LoanCalcRequestModel>(text);
                }
                this.Info("Returning deserializeed object.");
                return Ok(loanCalcModel.CreditContents);
            }
            catch (Exception ex)
            {
                this.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("save")]
        public async Task<ActionResult> Save([FromBody] LoanCalcRequestModel loanCalcModel)
        {
            try
            {
                Guid fileId = Guid.NewGuid();
                loanCalcModel.fileId = fileId.ToString();

                //saving the content if data is present
                if (loanCalcModel.MismoContent.content.Length > 0)
                {
                    //save content to disk and create unique fileid
                    string conn = $"{_configuration["FileStore:Connection"]}" + "\\" + fileId.ToString() + ".json";
                    string jsonContent = JsonConvert.SerializeObject(loanCalcModel);
                    this.Info("Saving data to file at connection:" + conn);
                    await System.IO.File.WriteAllTextAsync(conn, jsonContent);
                }
                else
                {
                    this.Info("loanCalcModel.MismoContent.content.Length cannot be zero. Bad Request.");
                    return BadRequest(loanCalcModel);
                }

                return CreatedAtAction("Save", loanCalcModel);
            }
            catch(Exception ex)
            {
                this.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}