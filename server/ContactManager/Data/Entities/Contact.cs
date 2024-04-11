using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        
        [MaxLength(15)]
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
