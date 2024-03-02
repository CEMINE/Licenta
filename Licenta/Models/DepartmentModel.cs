using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Models
{
    public class DepartmentModel
    {

        public int DepartmentID { get; set; }

        public string? DepartmentName { get; set;}
    }
}
