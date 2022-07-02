using CSVWriter.Attributes;

namespace CSVWriter.Models
{
    public class User
    {
        [CsvColumn(Name = "User Id")]
        public int Id { get; set; }
        [CsvColumn(Name = "User Name")]
        public string FirstName { get; set; }
        [CsvColumn(Name = "User Surname")]
        public string LastName { get; set; }
        [CsvColumn(Name = "User Password")]
        public string Password { get; set; }
        [CsvColumn(Name = "User Login")]
        public string Login { get; set; }
        [CsvColumn(Name = "Salary")] 
        public decimal Salary { get; set; }
    }
}