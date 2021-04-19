using System.ComponentModel.DataAnnotations;

namespace oop_CA.Models
{
    public enum SUBJECTS
    {
        MATHS, PHYSICS, PROGRAMMING, ENGLISH, ELECTRONICS
    }

    public class Mark
    {
        public int id { get; set; }
        [Display(Name = "Student Id")]
        public int studentId { get; set; }
        //public SUBJECTS subject { get; set; }
        public SUBJECTS subject = SUBJECTS.MATHS;
        [Display(Name = "Teacher Comment")]
        public string teacherComment { get; set; }
        [Display(Name = "Coefficient")]
        public int coefficient { get; set; }
        [Display(Name = "Value")]
        public int value { get; set; }
    }
}
