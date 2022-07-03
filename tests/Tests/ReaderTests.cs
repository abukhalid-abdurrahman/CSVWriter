using System.Collections.Generic;
using CSVWriter.CsvFileReader;

public class ReaderTests
{
    [Fact]
    public async Task ReadAsync()
    {
        var reader = new CsvFileReader();
        var result = await reader.ReadCsvDocumentLinesAsync("Fixtures/ReadingExample.csv");

        result
            .Should()
            .NotBeEmpty()
            .And.HaveCount(2) // Two lines
            .And.Equal(new List<string> { "hello,world", "test,example" }); // First line
    }
}
