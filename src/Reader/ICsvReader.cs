/*
 * Alif Tech LLC
 * Developed by Faridun Berdiev
 */

namespace CSVWriter.Reader;

public interface ICsvReader<T>
{
    List<T> ReadCsvRows(List<string> row, bool hasHeaders);
    Task<List<T>> ReadCsvRowsAsync(List<string> rows, bool hasHeaders);
}
