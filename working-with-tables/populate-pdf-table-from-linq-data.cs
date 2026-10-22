using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
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
            new Person { Id = 1, Name = "Alice",   Age = 30 },
            new Person { Id = 2, Name = "Bob",     Age = 25 },
            new Person { Id = 3, Name = "Charlie", Age = 35 }
        };

        // LINQ query: select Id, Name, Age where Age > 28
        var query = from p in people
                    where p.Age > 28
                    select new { p.Id, p.Name, p.Age };

        // Convert query result to a DataTable
        DataTable dt = QueryToDataTable(query);

        const string outputPath = "output.pdf";

        // Create PDF document and add a table populated from the DataTable
        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Create a Table layout object
            Table table = new Table
            {
                // Define column widths (adjust as needed)
                ColumnWidths = "80 200 60",
                // Optional: set a default border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Import the DataTable into the Table (include header row)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }

    // Helper: converts any IEnumerable<T> to a DataTable using reflection
    static DataTable QueryToDataTable<T>(IEnumerable<T> data)
    {
        DataTable table = new DataTable();

        var properties = typeof(T).GetProperties();

        // Create columns based on the properties of T
        foreach (var prop in properties)
        {
            Type colType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            table.Columns.Add(prop.Name, colType);
        }

        // Populate rows
        foreach (var item in data)
        {
            DataRow row = table.NewRow();
            foreach (var prop in properties)
            {
                object value = prop.GetValue(item, null) ?? DBNull.Value;
                row[prop.Name] = value;
            }
            table.Rows.Add(row);
        }

        return table;
    }

    // Simple POCO used for the example
    class Person
    {
        public int    Id   { get; set; }
        public string Name { get; set; }
        public int    Age  { get; set; }
    }
}