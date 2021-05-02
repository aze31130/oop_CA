using System.Collections.Generic;

namespace oop_CA.Models
{
    public class MarkViewModel
    {
        public List<Mark> allMarks { get; set; }
        public int bestMark { get; set; }
        public int worstMark { get; set; }
        public double average { get; set; }
    }
}
