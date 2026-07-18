using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "DataViewTable.pdf";

        // Prepare sample data in a DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Quantity", typeof(int));

        dt.Rows.Add(1, "Apples", 50);
        dt.Rows.Add(2, "Bananas", 30);
        dt.Rows.Add(3, "Cherries", 20);
        dt.Rows.Add(4, "Dates", 15);
        dt.Rows.Add(5, "Elderberries", 5);

        // Create a DataView from the DataTable (you can apply filtering here)
        DataView view = new DataView(dt);
        // Example filter: only rows where Quantity > 10
        view.RowFilter = "Quantity > 10";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set basic properties
            Table table = new Table
            {
                // Optional: set column widths (3 columns)
                ColumnWidths = "100 150 100",
                // Optional: set border for visual clarity
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Import the DataView into the table
            // Parameters:
            //   view                 : source DataView
            //   true                 : import column names as first row
            //   0                    : start at first row (zero‑based)
            //   0                    : start at first column (zero‑based)
            //   view.Count           : maximum rows to import
            //   dt.Columns.Count     : maximum columns to import
            table.ImportDataView(view, true, 0, 0, view.Count, dt.Columns.Count);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported DataView saved to '{outputPath}'.");
    }
}