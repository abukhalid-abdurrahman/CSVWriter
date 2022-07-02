using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CSVWriter.CsvFileWriter
{
    public class CsvFileWriter : ICsvFileWriter
    {
        /// <summary>
        /// Save csv data to file
        /// </summary>
        /// <param name="path">Document location on your disc</param>
        /// <param name="lines">Csv data lines</param>
        /// <returns>True, if file saved without exception, and false if come exception was thrown</returns>
        public async Task<bool> SaveCsvDocumentAsync(string path, params string[] lines) => await  TrySaveDocumentAsync(path, lines);
        
        private async Task<bool> TrySaveDocumentAsync(string path, params string[] lines)
        {
            try
            {
                var streamWriter = new StreamWriter(path, false, Encoding.UTF8);

                foreach (var line in lines)
                {
                    await streamWriter.WriteLineAsync(line);
                }
                await streamWriter.FlushAsync();
                streamWriter.Close();
                streamWriter.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}