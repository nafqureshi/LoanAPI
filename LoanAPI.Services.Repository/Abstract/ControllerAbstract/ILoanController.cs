using LoanAPI.Services.Repository.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LoanAPI.Services.Repository.Abstract.ControllerAbstract
{
    public interface ILoanController
    {
        ActionResult<IEnumerable<LoanDetail>> GetLoanDetails();

        ActionResult<LoanDetail> GetLoanDetail(int studentId, int institutionId);

        ActionResult PutLoanDetail(int studentId, int institutionId, LoanDetail loanDetail);

        ActionResult<LoanDetail> PostLoanDetail(LoanDetail loanDetail);
    }
}
