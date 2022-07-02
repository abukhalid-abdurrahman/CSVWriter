/*
 * Alif Tech LLC
 * Developed by Faridun Berdiev
 */

using System;

namespace CSVWriter.Attributes
{
    public class CsvColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }
}