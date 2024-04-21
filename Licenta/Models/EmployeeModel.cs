using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Licenta.Models
{
    public class EmployeeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [MaxLength(30)]
        public string? FirstName { get; set; }
        [MaxLength(30)]
        public string? LastName { get; set;}
        public int DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual DepartmentModel? Department { get; set; }
        public int JobID { get; set; }
        [ForeignKey("JobID")]
        public virtual JobModel? Job { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public string CVPath { get; set; }
        public int? Age
        {
            get
            {
                if (BirthDate.Year != null)
                {
                    DateTime today = DateTime.Today;
                    int age = today.Year - BirthDate.Year;

                    // Adjust age if birthday hasn't occurred yet this year
                    if (BirthDate > today.AddYears(-age))
                    {
                        age--;
                    }

                    return age;
                }

                return null; // or another default value if BirthDate is null
            }
            set
            {

            }
        }
        public int Seniority
        {
            get
            {
                DateTime today = DateTime.Today;
                int seniority = today.Year - HireDate.Year;

                // Adjust seniority if hire date hasn't occurred yet this year
                if (HireDate > today.AddYears(-seniority))
                {
                    seniority--;
                }

                return seniority;

            }
            set
            {

            }
        }        

    }
}
