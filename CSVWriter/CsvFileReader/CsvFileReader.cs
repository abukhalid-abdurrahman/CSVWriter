using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CSVWriter.CsvFileReader
{
    public class CsvFileReader : ICsvFileReader
    {
        /// <summary>
        /// Read csv file and returns csv lines
        /// </summary>
        /// <param name="documentFileName">Contains path to csv file</param>
        /// <returns>List of csv file lines</returns>
        public async Task<List<string>> ReadCsvDocumentAsync(string documentFileName) => await ReadDocument(documentFileName);

        private async Task<List<string>> ReadDocument(string documentFileName)
        {
            var data = new List<string>();
            using var reader = new StreamReader(documentFileName, Encoding.UTF8);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                data.Add(line);
            }
            return data;
        }
    }
}