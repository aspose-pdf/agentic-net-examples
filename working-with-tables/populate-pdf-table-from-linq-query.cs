using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Sample data source
        var people = new List<Person>
        {
            new Person { Id = 1, Name = "Alice", Age = 30 },
            new Person { Id = 2, Name = "Bob",   Age = 25 },
            new Person { Id = 3, Name = "Carol", Age = 28 }
        };

        // LINQ query
        var query = from p in people
                    where p.Age >= 26
                    select new { p.Id, p.Name, p.Age };

        // Convert query result to DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age", typeof(int));

        foreach (var item in query)
        {
            dt.Rows.Add(item.Id, item.Name, item.Age);
        }

        // Create PDF and add table
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table
            {
                // Optional: set column widths (example: three equal columns)
                ColumnWidths = "100 200 100"
            };

            // Import DataTable into the Aspose.Pdf.Table
            // Parameters: DataTable, import column names as first row, start at row 0, column 0
            table.ImportDataTable(dt, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with table created successfully.");
    }

    // Simple POCO for demonstration
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}