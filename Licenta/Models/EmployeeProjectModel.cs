
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Models
{

    public class EmployeeProjectModel
    {

        public int EmployeeId { get; set; }


        public int ProjectId { get; set; }


        public virtual EmployeeModel? Employee { get; set; }


        public virtual ProjectModel? Project { get; set; }
    }
}
