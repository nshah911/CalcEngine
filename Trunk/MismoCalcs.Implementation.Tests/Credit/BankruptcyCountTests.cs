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
    public class BankruptcyCountTests
    {
        [Fact]
        public void BankruptcyCount_within_120_months_should_be_counted()
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
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11, _FiledDate = DateTime.Parse("9/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyTypeUnknown, _FiledDate = DateTime.Parse("9/15/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Attachment, _FiledDate = DateTime.Parse("9/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11, _FiledDate = DateTime.Parse("9/16/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter7Involuntary, _FiledDate = DateTime.Parse("9/19/2008") });

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.BankruptcyCount(120);
            // Assert
            Assert.Equal<int>(4, result);
        }

        [Fact]
        public void BankruptcyCount_within_121_months_should_not_be_counted()
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
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11, _FiledDate = DateTime.Parse("9/14/2007") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyTypeUnknown, _FiledDate = DateTime.Parse("9/15/2007") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Attachment, _FiledDate = DateTime.Parse("9/14/2006") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11, _FiledDate = DateTime.Parse("9/16/2006") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter7Involuntary, _FiledDate = DateTime.Parse("9/19/2005") });

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.BankruptcyCount(120);
            // Assert
            Assert.Equal<int>(0, result);
        }

        [Fact]
        public void BankruptcyCount_within_119_months_should_be_counted()
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
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11, _FiledDate = DateTime.Parse("10/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyTypeUnknown, _FiledDate = DateTime.Parse("10/15/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Attachment, _FiledDate = DateTime.Parse("10/14/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11, _FiledDate = DateTime.Parse("10/16/2008") });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter7Involuntary, _FiledDate = DateTime.Parse("10/19/2008") });

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.BankruptcyCount(120);
            // Assert
            Assert.Equal<int>(4, result);
        }
        [Fact]
        public void BankruptcyCount_with_no_date_should_be_counted()
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
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11 });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyTypeUnknown, _FiledDate = DateTime.MinValue});
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.Attachment });
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter11});
            creditPublicRecords.Add(new CreditPublicRecord() { _Type = CreditPublicRecordType.BankruptcyChapter7Involuntary});

            creditResponseGroup.Response.ResponseData.CreditResponse.CreditPublicRecords = creditPublicRecords;
            creditResponseGroups.Add(creditResponseGroup);
            // Act
            CreditModelCommonCalcs creditModelCommonCalcs = new CreditModelCommonCalcs(creditResponseGroups);
            result = creditModelCommonCalcs.BankruptcyCount(120);
            // Assert
            Assert.Equal<int>(4, result);
        }
    }
}
