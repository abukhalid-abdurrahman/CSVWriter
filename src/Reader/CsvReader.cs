/*
 * Alif Tech LLC
 * Developed by Faridun Berdiev
 */

using System.Text.RegularExpressions;
using CSVWriter.Enums;
using CSVWriter.Extensions;

namespace CSVWriter.Reader;

public class CsvReader<T> : ICsvReader<T> where T : new()
{
    private CsvDelimiterType _delimiter;

    public CsvReader(CsvDelimiterType delimiter)
    {
        _delimiter = delimiter;
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
    public async Task<List<T>> ReadCsvRowsAsync(List<string> rows, bool hasHeaders) =>
        await Task.Run(() => ReadCsvRows(rows, hasHeaders));

    private List<T> ReadRows(List<string> rows, bool hasHeaders)
    {
        var data = new List<T>();

        for (var i = 0; i < rows.Count; i++)
        {
            if (i < 1 && hasHeaders)
                continue;

            var rowCells = rows[i].Split(';');
            rowCells = _delimiter == CsvDelimiterType.Comma ? SetCommaDelimiter(rows[i]) : rowCells;
            data.Add(SetModelProperty(rowCells));
        }

        return data;
    }

    private string[] SetCommaDelimiter(string row)
    {
        var cellBlockRegex = new Regex("\".*\"");
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
        var properties = typeof(T).GetProperties().ToList();

        var model = new T();

        for (var i = 0; i < properties.Count; i++)
        {
            model.SetPropertyValue(properties[i].Name, rowCells[i]);
        }
        return model;
    }
}
