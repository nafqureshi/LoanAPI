using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanAPI.Data.DBRepository.Entity
{
    public class Loan
    {
        [Key]
        public int StudentId { get; set; }

        [Key]
        public int InstitutionId { get; set; }

        public decimal LoanAmount { get; set; }
    }
}
