using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Sample data source
        var people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30 },
            new Person { Name = "Bob",   Age = 25 },
            new Person { Name = "Carol", Age = 28 }
        };

        // LINQ query
        var query = from p in people
                    where p.Age >= 25
                    select new { p.Name, p.Age };

        // Convert query result to DataTable
        DataTable dataTable = new DataTable();
        // Add columns based on the anonymous type properties
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));

        // Populate rows
        foreach (var item in query)
        {
            dataTable.Rows.Add(item.Name, item.Age);
        }

        // Create PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table
            {
                // Position the table on the page (optional)
                // Left and Top are measured in points (1/72 inch)
                Left = 50,
                Top = 700,
                // Define column widths (optional)
                ColumnWidths = "200 100"
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters:
            //   dataTable               – source DataTable
            //   true                    – import column names as first row
            //   0                       – start at first row of the table (zero‑based)
            //   0                       – start at first column of the table (zero‑based)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF with populated table saved as 'output.pdf'.");
    }

    // Helper to detect missing libgdiplus/DLL
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

    // Simple POCO for demonstration
    class Person
    {
        // Initialise to avoid CS8618 warning for non‑nullable reference type
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
