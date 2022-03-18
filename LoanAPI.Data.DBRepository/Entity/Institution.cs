using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanAPI.Data.DBRepository.Entity
{
    public class Institution
    {
        [Key]
        public int InstitutionId { get; set; }

        public string Name { get; set; }
    }
}
