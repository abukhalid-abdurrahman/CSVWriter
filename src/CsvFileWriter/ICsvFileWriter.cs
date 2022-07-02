namespace CSVWriter.CsvFileWriter;

public interface ICsvFileWriter
{
    Task<bool> SaveCsvDocumentAsync(string path, params string[] lines);
}
