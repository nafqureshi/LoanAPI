
using System.Text.Json.Serialization;

namespace LoanAPI.Services.Repository.Model
{
    public class LoanDetail
    {
        public int StudentId { get; set; }

        public int InstitutionId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string InstitutionName { get; set; }

        public int LoanAmount { get; set; }
    }
}
