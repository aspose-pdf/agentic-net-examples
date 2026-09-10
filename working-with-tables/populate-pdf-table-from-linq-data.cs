using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for text-related types if needed

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

        // LINQ query to select and order data
        var query = from p in people
                    where p.Age >= 25
                    orderby p.Name
                    select new { p.Id, p.Name, p.Age };

        // Convert the query result to a DataTable
        DataTable dataTable = ToDataTable(query);

        // Create a new PDF document (lifecycle: create)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure basic appearance
            Table table = new Table
            {
                // Define three column widths (adjust as needed)
                ColumnWidths = "100 200 100",
                // Add a thin black border around each cell
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: DataTable, import column names as first row, start at row 0, column 0
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (lifecycle: save)
            doc.Save("output.pdf");
        }
    }

    // Helper method: converts an IEnumerable<T> to a DataTable
    static DataTable ToDataTable<T>(IEnumerable<T> data)
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

    // Simple POCO representing a person
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}