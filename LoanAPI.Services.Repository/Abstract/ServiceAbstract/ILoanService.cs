using LoanAPI.Services.Repository.Model;
using System.Collections.Generic;

namespace LoanAPI.Services.Repository.Abstract.ServiceAbstract
{
    public interface ILoanService
    {
        IEnumerable<LoanDetail> GetLoanDetails();

        LoanDetail GetLoanDetail(int studentId, int institutionId);

        void PutLoanDetail(LoanDetail loanDetail);

        LoanDetail PostLoanDetail(LoanDetail loanDetail);

        bool LoanDetailExists(int studentId, int institutionId);
    }
}
