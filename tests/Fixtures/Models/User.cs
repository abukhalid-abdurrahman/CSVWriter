using CSVWriter.Attributes;

namespace CSVWriter.Models;

public class User
{
    [CsvColumn(Name = "User Id")]
    public int Id { get; set; }

    [CsvColumn(Name = "User Name")]
    public string FirstName { get; set; } = string.Empty;

    [CsvColumn(Name = "User Surname")]
    public string LastName { get; set; } = string.Empty;

    [CsvColumn(Name = "User Password")]
    public string Password { get; set; } = string.Empty;

    [CsvColumn(Name = "User Login")]
    public string Login { get; set; } = string.Empty;

    [CsvColumn(Name = "Salary")]
    public decimal Salary { get; set; }
}
