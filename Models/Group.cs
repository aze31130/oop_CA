using System.ComponentModel.DataAnnotations;

namespace oop_CA.Models
{
    public class Group
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int referentTeacherId { get; set; }
    }
}
