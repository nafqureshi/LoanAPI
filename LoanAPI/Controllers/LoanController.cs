using LoanAPI.Services.Repository.Abstract.ControllerAbstract;
using LoanAPI.Services.Repository.Abstract.ServiceAbstract;
using LoanAPI.Services.Repository.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase, ILoanController
    {
        public ILoanService LoanService { get; set; }

        // GET: api/Loan/studentId/5/institutionId/6
        [HttpGet("studentId/{studentId}/institutionId/{institutionId}")]
        public ActionResult<LoanDetail> GetLoanDetail(int studentId, int institutionId)
        {
            var loanDetail = LoanService.GetLoanDetail(studentId, institutionId);

            if (loanDetail == null)
            {
                return NotFound();
            }

            return loanDetail;
        }

        // GET: api/Loan
        [HttpGet]
        public ActionResult<IEnumerable<LoanDetail>> GetLoanDetails()
        {
            return LoanService.GetLoanDetails().ToList();
        }

        // POST: api/PostLoanDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<LoanDetail> PostLoanDetail(LoanDetail loanDetail)
        {
            LoanService.PostLoanDetail(loanDetail);

            return CreatedAtAction("GetLoanDetail", new { studentId = loanDetail.StudentId, institutionId = loanDetail.InstitutionId }, loanDetail);
        }

        // PUT: api/Loan/studentId/5/institutionId/6
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("studentId/{studentId}/institutionId/{institutionId}")]
        public ActionResult PutLoanDetail(int studentId, int institutionId, LoanDetail loanDetail)
        {
            if (studentId != loanDetail.StudentId && institutionId != loanDetail.InstitutionId)
            {
                return BadRequest();
            }

            try
            {
                LoanService.PutLoanDetail(loanDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanService.LoanDetailExists(studentId, institutionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
