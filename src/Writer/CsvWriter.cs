/*
 * Alif Tech LLC
 * Developed by Faridun Berdiev
 */

using System.Dynamic;
using System.Reflection;
using CSVWriter.Attributes;
using CSVWriter.Enums;

namespace CSVWriter.Writer;

public class CsvWriter<T> : ICsvWriter<T>
{
    /// <summary>
    /// Contains data in csv format that was added during the use of the WriteLine method
    /// </summary>
    /// <returns>Stream that can be used for writing it to file</returns>
    public List<string> GetCsvRowsData() => GetRows();

    /// <summary>
    /// Contains data in csv format that was added during the use of the WriteLineAsync method
    /// </summary>
    /// <returns>Stream that can be used for writing it to file</returns>
    public async Task<List<string>> GetCsvRowsDataAsync() => await Task.Run(GetCsvRowsData);

    /// <summary>
    /// Writes CSV lines to file asynchronously
    /// </summary>
    /// <param name="path">Location of file</param>
    /// <returns>True, if file saved without exception, and false if come exception was thrown</returns>
    public async Task<bool> SaveDocumentAsync(string path) => await WriteCsvDataToFile(path);

    /// <summary>
    /// Writes model to an array of data
    /// </summary>
    /// <param name="model">The model that will be written to an array of data that can be exported via the GetFileStream method</param>
    public void WriteLine(T model) => WriteToList(model);

    /// <summary>
    /// Writes model to an array of data asynchronously
    /// </summary>
    /// <param name="model">The model that will be written to an array of data that can be exported via the GetFileStreamAsync method</param>
    public async Task WriteLineAsync(T model) => await Task.Run(() => WriteLine(model));

    private static readonly List<IDictionary<string, object>> _csvData = new();
    private static CsvDelimiterType _delimiterType;

    public CsvWriter(CsvDelimiterType delimiterType)
    {
        _delimiterType = delimiterType;
        WriteHeaders();
    }

    private static List<string> GetRows()
    {
        try
        {
            var rows = new List<string>();
            foreach (var element in _csvData)
            {
                var rowElements = element.Values
                    .Select(x => x.ToString().Replace("\"", "\"\""))
                    .ToList();
                var rowStr = string.Join(
                    _delimiterType == CsvDelimiterType.Comma ? "," : ";",
                    rowElements.Select(
                        x =>
                            x.Contains(",") && _delimiterType == CsvDelimiterType.Comma
                                ? "\"" + x + "\""
                                : x.Replace(';', '.')
                    )
                );

                rows.Add(rowStr);
            }

            return rows;
        }
        catch
        {
            return new List<string>();
        }
    }

    private static async Task<bool> WriteCsvDataToFile(string path)
    {
        var lines = GetRows().ToArray();
        var writer = new CsvFileWriter.CsvFileWriter();
        return await writer.SaveCsvDocumentAsync(path, lines);
    }

    private static void WriteToList(T model)
    {
        if (model == null)
            return;

        var expandoObject = new ExpandoObject() as IDictionary<string, object>;
        var modelProperties = typeof(T).GetProperties().ToList();
        modelProperties.ForEach(
            x =>
            {
                expandoObject.Add(x.Name, x.GetValue(model));
            }
        );
        _csvData.Add(expandoObject);
    }

    private static void WriteHeaders()
    {
        var modelProperties = typeof(T).GetProperties().ToList();

        var expandoObject = new ExpandoObject() as IDictionary<string, object>;
        modelProperties.ForEach(
            x =>
            {
                var columnAttribute = (CsvColumnAttribute)x.GetCustomAttribute(
                    typeof(CsvColumnAttribute),
                    false
                );
                expandoObject.Add(x.Name, columnAttribute == null ? x.Name : columnAttribute.Name);
            }
        );
        _csvData.Add(expandoObject);
    }
}
