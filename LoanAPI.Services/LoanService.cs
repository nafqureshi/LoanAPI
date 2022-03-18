using LoanAPI.Data.DBRepository.Concrete;
using LoanAPI.Data.DBRepository.Entity;
using LoanAPI.Services.Repository.Abstract.ServiceAbstract;
using LoanAPI.Services.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly StudentLoanDbContext context;

        private Student student;

        private Institution institution;

        private Loan loan;

        private IEnumerable<LoanDetail> loanDetails;

        public LoanService(StudentLoanDbContext context)
        {
            this.context = context;            
            loanDetails = FetchData();
        }

        public LoanDetail GetLoanDetail(int studentId, int institutionId)
        {
            return loanDetails.ToList().FirstOrDefault(ld => ld.StudentId == studentId && ld.InstitutionId == institutionId);
        }

        public IEnumerable<LoanDetail> GetLoanDetails()
        {
            return loanDetails;
        }

        public LoanDetail PostLoanDetail(LoanDetail loanDetail)
        {
            student = new Student
            {
                StudentId = loanDetail.StudentId,
                FirstName = loanDetail.FirstName,
                LastName = loanDetail.LastName
            };

            institution = new Institution
            {
                InstitutionId = loanDetail.InstitutionId,
                Name = loanDetail.InstitutionName
            };

            loan = new Loan
            {
                StudentId = loanDetail.StudentId,
                InstitutionId = loanDetail.InstitutionId,
                LoanAmount = loanDetail.LoanAmount
            };

            context.InsertLoanDetails(student, institution, loan);

            loanDetail.StudentId = student.StudentId;
            loanDetail.InstitutionId = institution.InstitutionId;

            return loanDetail;
        }

        public void PutLoanDetail(LoanDetail loanDetail)
        {
            student = new Student
            {
                StudentId = loanDetail.StudentId,
                FirstName = loanDetail.FirstName,
                LastName = loanDetail.LastName
            };

            institution = new Institution
            {
                InstitutionId = loanDetail.InstitutionId,
                Name = loanDetail.InstitutionName
            };

            loan = new Loan
            {
                StudentId = loanDetail.StudentId,
                InstitutionId = loanDetail.InstitutionId,
                LoanAmount = loanDetail.LoanAmount
            };

            context.UpdateLoanDetails(student, institution, loan);
        }

        public bool LoanDetailExists(int studentId, int institutionId)
        {
            loanDetails = FetchData();
            return loanDetails.Any(ld => ld.StudentId == studentId && ld.InstitutionId == institutionId);
        }


        private IEnumerable<LoanDetail> FetchData()
        {
            return context.Loan.AsNoTracking().Join(context.Student.AsNoTracking(),
                                                    loan => loan.StudentId,
                                                    student => student.StudentId,
                                                    (loan, student) => new LoanDetail
                                                    {
                                                        StudentId = student.StudentId,
                                                        FirstName = student.FirstName,
                                                        LastName = student.LastName,
                                                        LoanAmount = loan.LoanAmount,
                                                        InstitutionId = loan.InstitutionId
                                                    })
                                                .Join(context.Institution.AsNoTracking(),
                                                    loanDetail => loanDetail.InstitutionId,
                                                    institution => institution.InstitutionId,
                                                    (loanDetail, institution) => new LoanDetail
                                                    {
                                                        StudentId = loanDetail.StudentId,
                                                        FirstName = loanDetail.FirstName,
                                                        LastName = loanDetail.LastName,
                                                        LoanAmount = loanDetail.LoanAmount,
                                                        InstitutionId = loanDetail.InstitutionId,
                                                        InstitutionName = institution.Name
                                                    });
        }

    }
}
