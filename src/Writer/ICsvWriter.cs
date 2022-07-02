/*
 * Alif Tech LLC
 * Developed by Faridun Berdiev
 */

namespace CSVWriter.Writer;

public interface ICsvWriter<in T>
{
    void WriteLine(T model);
    Task WriteLineAsync(T model);
    List<string> GetCsvRowsData();
    Task<List<string>> GetCsvRowsDataAsync();
    Task<bool> SaveDocumentAsync(string path);
}
