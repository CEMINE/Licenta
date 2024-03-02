using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Licenta.Models
{
    public class JobModel
    {

        public int JobID { get; set; }
        public string? JobTitle { get; set; }
        public double Base_Salary { get; set; }
    }
}
