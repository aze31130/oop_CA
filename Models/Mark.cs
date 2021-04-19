using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oop_CA.Models
{
    public enum SUBJECTS
    {
        MATHS, PHYSICS, PROGRAMMING, ENGLISH, ELECTRONICS
    }

    public class Mark
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public SUBJECTS subject { get; set; }
        public string comment { get; set; }
        public int coefficient { get; set; }
        public int value { get; set; }
    }
}
