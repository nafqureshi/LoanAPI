using DbContext = LoanAPI.Data.DBRepository.Concrete.StudentLoanDbContext;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using LoanAPI.Data.DBRepository.Concrete;
using Moq;
using LoanAPI.Services.Repository.Model;
using LoanAPI.Data.DBRepository.Entity;
using System.Collections.Generic;

namespace LoanAPI.Services.Unit.Test
{
    public class Tests
    {
        private DbContext context;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoanAPIService_Post_Success()
        {
            //Setup
            context = new DbContext(new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("LoanAPIService_Success").Options);

            var expectedOutput = new LoanDetail
            {
                StudentId = 1,
                InstitutionId = 1,
                FirstName = "Abdun",
                LastName = "Nafay",
                InstitutionName = "fin",
                LoanAmount = 1000
            };


            var input = new LoanDetail
            {
                StudentId = 0,
                InstitutionId = 0,
                FirstName = "Abdun",
                LastName = "Nafay",
                InstitutionName = "fin",
                LoanAmount = 1000
            };

            //Act
            var loanService = new LoanService(context);
            var response = loanService.PostLoanDetail(input);

            //Assert
            Assert.AreEqual(expectedOutput.StudentId, response.StudentId);
            Assert.AreEqual(expectedOutput.InstitutionId, response.InstitutionId);
        }
    }
}