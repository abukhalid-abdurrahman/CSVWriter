namespace CSVWriter.CsvFileReader;

public interface ICsvFileReader
{
    Task<List<string>> ReadCsvDocumentLinesAsync(string documentFileName);
}
