using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare in‑memory data
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add(1, "Apple", 0.99m);
        dt.Rows.Add(2, "Banana", 0.59m);
        dt.Rows.Add(3, "Cherry", 2.49m);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create the table (parameterless constructor) and configure it
            Table table = new Table();
            table.ColumnWidths = "100 200 100"; // optional column width specification
            // Optional: add a simple border to all cells
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black);

            // Import the DataTable rows (include column headers)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("DataTableExport.pdf");
        }

        Console.WriteLine("PDF with imported DataTable saved as 'DataTableExport.pdf'.");
    }
}