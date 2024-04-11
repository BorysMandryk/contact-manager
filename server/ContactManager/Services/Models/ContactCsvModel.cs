using CsvHelper.Configuration.Attributes;

namespace Infrastructure.Models
{
    internal class ContactCsvModel
    {
        public string Name { get; set; }

        [Format("dd.MM.yyyy")]
        public DateTime DateOfBirth { get; set; }

        [BooleanTrueValues("yes")]
        [BooleanFalseValues("no")]
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
