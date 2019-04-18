using CalcEngine.Models.Credit;
using CalcEngine.Models.Credit_v24;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcEngine.Models.Mapper.Credit
{
    public class CreditMapManager
    {
        private readonly CREDITREPORTING_RESPONSE_GROUP_Type _creditXml = null;
        public CreditMapManager(CREDITREPORTING_RESPONSE_GROUP_Type creditXml)
        {
            _creditXml = creditXml;
        }


        public CreditResponseGroup MapToCredit()
        {
            CreditResponseGroup creditResponseGroup = new CreditResponseGroup();
            Response response = new Response();
            ResponseData responseData = new ResponseData();
            CreditResponse creditResponse = new CreditResponse();

            if (_creditXml != null)
            {
                creditResponse.CreditLiabilities = CreditLiability();
                creditResponse.CreditScores = CreditScore();
                creditResponse.CreditPublicRecords = CreditPublicRecords();
                creditResponse.PrimaryBorrower = PrimaryBorrower();
                creditResponse.CoBorrower = CoBorrower();

                response = Response();

                responseData.CreditResponse = creditResponse;
                response.ResponseData = responseData;
                creditResponseGroup.Response = response;
            }
            
            return creditResponseGroup;
        }

        private Response Response()
        {
            Response response = new Response();
            if (_creditXml != null
                && _creditXml.RESPONSE != null)
            {
                ResponseMapper map = new ResponseMapper(_creditXml.RESPONSE[0]);
                response = map.Convert();
            }
            return response;
        }


        private ResponseData ResponseData()
        {
            ResponseData responseData = new ResponseData();
            if (_creditXml != null
                && _creditXml.RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA != null)
            {
                ResponseDataMapper map = new ResponseDataMapper(_creditXml.RESPONSE[0].RESPONSE_DATA[0]);
                responseData = map.Convert();
            }
            return responseData;
        }

        private CreditResponse CreditResponses()
        {
            CreditResponse creditResponse = new CreditResponse();
            if (_creditXml != null
                && _creditXml.RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0] != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE != null)
            {
                CreditResponseMapper map = new CreditResponseMapper(_creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE);
                creditResponse = map.Convert();
            }
            return creditResponse;
        }

        private List<CreditLiability> CreditLiability()
        {
            List<CreditLiability> list = new List<CreditLiability>();
            //CreditLiability dto = null;
            if (_creditXml != null
                && _creditXml.RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0] != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.CREDIT_LIABILITY != null)
            {
                Parallel.For(0, _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.CREDIT_LIABILITY.Length,
                    x => 
                    {
                        CreditLiabilityMapper creditLiabilityMapper = new CreditLiabilityMapper(_creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.CREDIT_LIABILITY[x]);
                        list.Add(creditLiabilityMapper.Convert());
                    });
            }
            return list;
        }


        private List<CreditPublicRecord> CreditPublicRecords()
        {
            List<CreditPublicRecord> list = new List<CreditPublicRecord>();
            if(_creditXml != null
                && _creditXml.RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0] != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.CREDIT_PUBLIC_RECORD != null)
            {
                CreditPublicRecordMapper map = new CreditPublicRecordMapper(_creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.CREDIT_PUBLIC_RECORD);
                list = map.Convert();   
            }
            return list;
        }


        private List<CreditScore> CreditScore()
        {
            List<CreditScore> list = new List<CreditScore>();
            if (_creditXml != null
                && _creditXml.RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0] != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.CREDIT_SCORE != null)
            {
                CreditScoreMapper map = new CreditScoreMapper(_creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.CREDIT_SCORE);
                list = map.Convert();
            }
            return list;
        }


        private Borrower PrimaryBorrower()
        {
            Borrower borrower = null;
            if (_creditXml != null
                && _creditXml.RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0] != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.BORROWER != null)
            {
                foreach(CREDITREPORTING_CREDIT_BORROWER_Type PrimBorrower in _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.BORROWER)
                {
                    if(PrimBorrower._PrintPositionType == CREDITREPORTING_BorrowerPrintPositionTypeEnumerated.Borrower)
                    {
                        BorrowerMapper map = new BorrowerMapper(PrimBorrower);
                        borrower = map.Convert();
                        if(PrimBorrower._RESIDENCE != null)
                        {
                            ResidenceMapper residenceMap = new ResidenceMapper(PrimBorrower._RESIDENCE[0]);
                            borrower._Residence = residenceMap.ConvertResidence();
                        }
                    }
                    
                }
            }
            return borrower;
        }

        private Borrower CoBorrower()
        {
            Borrower borrower = null;
            if (_creditXml != null
                && _creditXml.RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0] != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE != null
                && _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.BORROWER != null)
            {
                foreach (CREDITREPORTING_CREDIT_BORROWER_Type CoBorrower in _creditXml.RESPONSE[0].RESPONSE_DATA[0].CREDIT_RESPONSE.BORROWER)
                {
                    if (CoBorrower._PrintPositionType == CREDITREPORTING_BorrowerPrintPositionTypeEnumerated.CoBorrower)
                    {
                        BorrowerMapper map = new BorrowerMapper(CoBorrower);
                        borrower = map.Convert();
                        if (CoBorrower._RESIDENCE != null)
                        {
                            ResidenceMapper residenceMap = new ResidenceMapper(CoBorrower._RESIDENCE[0]);
                            borrower._Residence = residenceMap.ConvertResidence();
                        }
                    }

                }
            }
            return borrower;
        }

    }
}
