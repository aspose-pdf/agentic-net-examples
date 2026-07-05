using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for BorderInfo if needed

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

        // LINQ query to select required fields
        var query = from p in people
                    where p.Age >= 25
                    orderby p.Name
                    select new { p.Id, p.Name, p.Age };

        // Convert the query result to a DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("Id",   typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age",  typeof(int));

        foreach (var row in query)
        {
            dt.Rows.Add(row.Id, row.Name, row.Age);
        }

        // Create a PDF document (lifecycle rule: use using and Save)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set basic formatting
            Table table = new Table
            {
                // Simple column width definition (in points)
                ColumnWidths = "100 200 100",
                // Add a thin black border to each cell
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: DataTable, import column names as first row, start at row 0, column 0
            table.ImportDataTable(dt, true, 0, 0);

            // Add the populated table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (lifecycle rule)
            doc.Save("output.pdf");
        }
    }

    // Simple POCO used for the LINQ source
    class Person
    {
        public int    Id   { get; set; }
        public string Name { get; set; }
        public int    Age  { get; set; }
    }
}