using System;
using System.ComponentModel.DataAnnotations;
using static oop_CA.Models.Enumeration;

namespace oop_CA.Models
{
    public class Mark
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Student Id")]
        public int studentId { get; set; }
        public SUBJECT subject { get; set; }
        [Display(Name = "Teacher Comment")]
        public string teacherComment { get; set; }
        [Display(Name = "Coefficient")]
        public int coefficient { get; set; }
        [Display(Name = "Value")]
        public int value { get; set; }
        public DateTime date { get; set; }
    }
}
