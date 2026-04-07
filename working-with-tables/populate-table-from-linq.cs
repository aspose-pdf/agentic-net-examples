using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;

namespace PopulateTableFromLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample data source
            List<Person> people = new List<Person>()
            {
                new Person(){ Id = 1, Name = "Alice", Age = 30 },
                new Person(){ Id = 2, Name = "Bob", Age = 25 },
                new Person(){ Id = 3, Name = "Charlie", Age = 35 }
            };

            // LINQ query
            IEnumerable<Person> query = people.Where(p => p.Age >= 25).OrderBy(p => p.Name);

            // Convert query result to DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));

            foreach (Person p in query)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = p.Id;
                row["Name"] = p.Name;
                row["Age"] = p.Age;
                dataTable.Rows.Add(row);
            }

            // Create PDF document
            using (Document doc = new Document())
            {
                // Add a page
                Page page = doc.Pages.Add();

                // Create a table with three columns
                Table table = new Table();
                // Ensure three columns exist before import
                Row headerRow = table.Rows.Add();
                headerRow.Cells.Add(new Cell());
                headerRow.Cells.Add(new Cell());
                headerRow.Cells.Add(new Cell());

                // Import DataTable into the table (including column names as first row)
                table.ImportDataTable(dataTable, true, 0, 0);

                // Add table to page
                page.Paragraphs.Add(table);

                // Save PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                string outputPath = "output.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                else
                {
                    try
                    {
                        doc.Save(outputPath);
                        Console.WriteLine($"PDF saved to '{outputPath}'. (Non‑Windows platform, libgdiplus may be required.)");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                    }
                }
            }
        }

        // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
        private static bool ContainsDllNotFound(Exception? ex)
        {
            while (ex != null)
            {
                if (ex is DllNotFoundException)
                    return true;
                ex = ex.InnerException;
            }
            return false;
        }

        // Simple POCO class used in the LINQ query
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
