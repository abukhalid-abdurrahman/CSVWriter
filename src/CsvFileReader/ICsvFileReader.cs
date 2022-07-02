using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVWriter.CsvFileReader
{
    public interface ICsvFileReader
    {
        Task<List<string>> ReadCsvDocumentAsync(string documentFileName);
    }
}