
public class WrtiterTests
{
    private readonly string[] data = new string[] { "first,line", "second,line" };

    public Task CreateTheFileAsync(string fileName)
    {
        var writer = new CsvFileWriter();
        return writer.SaveCsvDocumentAsync(fileName, data);
    }

    [Fact]
    public async Task WriteFileTestAsync()
    {
        var fileName = "test.csv";
        await CreateTheFileAsync(fileName);

        // Check if created successfully
        Assert.True(File.Exists(fileName));

        var reader = new CsvFileReader();
        var lines = await reader.ReadCsvDocumentLinesAsync(fileName);
        lines.Should().NotBeEmpty().Equals(data);
    }
}
