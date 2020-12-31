/*
 * Alif Tech LLC
 * Developed by Faridun Berdiev
 */

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSVWriter.Enums;
using CSVWriter.Extensions;

namespace CSVWriter.Reader
{
    public class CsvReader<T> : ICsvReader<T> where T : new()
    {
        private CsvDelimetterType _delimetter;

        public CsvReader(CsvDelimetterType delimetter)
        {
            _delimetter = delimetter;
        }

        /// <summary>
        /// Reads rows in csv format and create from it a list of model type of T
        /// </summary>
        /// <param name="rows">List of rows in csv format</param>
        /// <param name="hasHeaders">Does input data have headers</param>
        /// <returns>List of model</returns>
        public List<T> ReadCsvRows(List<string> rows, bool hasHeaders) => ReadRows(rows, hasHeaders);

        /// <summary>
        /// Reads rows in csv format and create from it a list of model type of T asynchronously
        /// </summary>
        /// <param name="rows">List of rows in csv format</param>
        /// <param name="hasHeaders">Does input data have headers</param>
        /// <returns>List of model</returns>
        public async Task<List<T>> ReadCsvRowsAsync(List<string> rows, bool hasHeaders) => await Task.Run(() => ReadCsvRows(rows, hasHeaders));

        private List<T> ReadRows(List<string> rows, bool hasHeaders)
        {
            List<T> data = new List<T>();

            for (int i = 0; i < rows.Count; i++)
            {
                if (i < 1 && hasHeaders)
                    continue;
                
                string[] rowCells = rows[i].Split(';');
                rowCells = _delimetter == CsvDelimetterType.Comma ? SetCommaDelimetter(rows[i]) : rowCells;
                data.Add(SetModelProperty(rowCells));
            }

            return data;
        }

        private string[] SetCommaDelimetter(string row)
        {
            Regex cellBlockRegex = new Regex("\".*\"");
            var matchResult = cellBlockRegex.Match(row);
            if (!string.IsNullOrEmpty(matchResult.Value))
            {
                var lastMatch = matchResult.Value;
                var newMatch = matchResult.Value.Replace(",", ".");
                row = row.Replace(lastMatch, newMatch);
            }
            return row.Split(',');
        }

        private T SetModelProperty(string[] rowCells)
        {
            var properties = typeof(T)
                .GetProperties()
                .ToList();
                
            var model = new T();

            for (int i = 0; i < properties.Count; i++)
            {
                model.SetPropertyValue(properties[i].Name, rowCells[i]);
            }
            return model;
        }
    }
}