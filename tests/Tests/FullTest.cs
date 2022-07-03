namespace tests;

public class FullTest
{
    [Fact]
    public async Task Test_Read_Write()
    {
        var user = new User()
        {
            Id = 1,
            Login = "faha",
            Password = "faha001",
            FirstName = "Faridun",
            LastName = "Berdiev",
            Salary = 234.98m
        };

        var user1 = new User()
        {
            Id = 2,
            Login = "ramz",
            Password = "ramz001",
            FirstName = "Ramz",
            LastName = "Nazarov",
            Salary = 2345.98m
        };

        var writer = new CsvWriter<User>(CsvDelimiterType.Comma);

        writer.WriteLine(user);
        writer.WriteLine(user1);
        writer.WriteLine(user);
        writer.WriteLine(user1);
        writer.WriteLine(user);
        writer.WriteLine(user1);
        writer.WriteLine(user);
        writer.WriteLine(user1);
        writer.WriteLine(user);
        writer.WriteLine(user1);
        await writer.SaveDocumentAsync("test.csv");

        // Assert
        Assert.True(FileComparator.FileEquals("Fixtures/WriteTestResult.csv", "test.csv"));
    }
}
