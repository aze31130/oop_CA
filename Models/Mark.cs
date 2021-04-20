using System.ComponentModel.DataAnnotations;

namespace oop_CA.Models
{
    public enum SUBJECTS : int
    {
        MATHS = 1,
        PHYSICS = 2,
        PROGRAMMING = 3,
        ENGLISH = 4,
        ELECTRONICS = 5
    }
    public class Mark
    {
        public int id { get; set; }
        [Display(Name = "Student Id")]
        public int studentId { get; set; }
        public SUBJECTS subject { get; set; }
        [Display(Name = "Teacher Comment")]
        public string teacherComment { get; set; }
        [Display(Name = "Coefficient")]
        public int coefficient { get; set; }
        [Display(Name = "Value")]
        public int value { get; set; }
    }
}
