using CalcEngine.API.App_Code.FileManager;
using CalcEngine.Models.Mismo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CalcEngine.Models.Credit;
using CalcEngine.Models.Requests;

namespace CalcEngine.API.App_Code
{
    public class FileManagerApiClient : IGetContentRequest
    {
        public FileManagerApiClient()
        {
        }

        public async Task<List<CreditContentModel>> GetCreditContentAsync(string fileId, string apiUrlBase)
        {
            string url = apiUrlBase + "/filecontent/read/creditcontent/" + fileId;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string result = string.Empty;
            using (WebResponse resp = await request.GetResponseAsync() as HttpWebResponse)
            {
                using (Stream responseStream = resp.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            List<CreditContentModel> contentModels = JsonConvert.DeserializeObject<List<CreditContentModel>>(result);

            return contentModels;
        }

        public async Task<MismoContentModel> GetMismoContentAsync(string fileId, string apiUrlBase)
        {
            string url = apiUrlBase + "/filecontent/read/mismocontent/" + fileId;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string result = string.Empty;
            using (WebResponse resp = await request.GetResponseAsync() as HttpWebResponse)
            {
                using (Stream responseStream = resp.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            MismoContentModel contentModel = JsonConvert.DeserializeObject<MismoContentModel>(result);

            return contentModel;
        }

        public async Task<LoanCalcRequestModel> GetLoanCalcRequestAsync(string fileId, string apiUrlBase)
        {
            string url = apiUrlBase + "/filecontent/read/" + fileId;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            string result = string.Empty;
            using (WebResponse resp = await request.GetResponseAsync() as HttpWebResponse)
            {
                using (Stream responseStream = resp.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            LoanCalcRequestModel contentModel = JsonConvert.DeserializeObject<LoanCalcRequestModel>(result);

            return contentModel;
        }
    }
}
