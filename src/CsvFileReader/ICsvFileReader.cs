namespace CSVWriter.CsvFileReader;

public interface ICsvFileReader
{
    Task<List<string>> ReadCsvDocumentAsync(string documentFileName);
}
