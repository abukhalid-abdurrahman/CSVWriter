using System;
using System.Collections.Generic;
using CSVWriter.Enums;
using CSVWriter.Models;
using CSVWriter.Reader;
using CSVWriter.Writer;

namespace CSVWriter
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            User user = new User()
            {
                Id = 1,
                Login = "faha",
                Password = "faha001",
                FirstName = "Faridun",
                LastName = "Berdiev",
                Salary = 234.98m
            };
            
            User user1 = new User()
            {
                Id = 2,
                Login = "ramz",
                Password = "ramz001",
                FirstName = "Ramz",
                LastName = "Nazarov", 
                Salary = 2345.98m
            };

            CsvWriter<User> writer = new CsvWriter<User>(CsvDelimetterType.Comma);
            writer.WriteLine(user);
            writer.WriteLine(user1);
            writer.WriteLine(user);
            writer.WriteLine(user1);
            writer.WriteLine(user);
            writer.WriteLine(user1);
            writer.WriteLine(user);
            writer.WriteLine(user1);
            writer.WriteLine(user);
            writer.WriteLine(user1);
            writer.SaveDocumentAsync("test.csv").GetAwaiter().GetResult();

            CsvFileReader.CsvFileReader fileReader = new CsvFileReader.CsvFileReader();
            var data = fileReader.ReadCsvDocumentAsync("test.csv").GetAwaiter().GetResult();
            CsvReader<User> reader = new CsvReader<User>(CsvDelimetterType.Comma);
            var res = reader.ReadCsvRows(data, true);

            foreach (var item in res)
            {
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.Salary);
            }
        }
    }
}