using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement to use Aspose.Pdf.Facades

class Program
{
    static void Main()
    {
        // Prepare sample data in a DataTable
        DataTable data = new DataTable("Sample");
        data.Columns.Add("Product", typeof(string));
        data.Columns.Add("Quantity", typeof(int));
        data.Columns.Add("Price", typeof(decimal));

        data.Rows.Add("Apple", 10, 0.99m);
        data.Rows.Add("Banana", 5, 0.59m);
        data.Rows.Add("Cherry", 20, 2.49m);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table();

            // Define column widths (space‑separated values, in points)
            table.ColumnWidths = "100 100 100";

            // Set a border for the whole table
            table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);

            // Set a default border for each cell
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray);

            // Optional: set default cell padding
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Import the DataTable into the PDF table.
            // Parameters: (DataTable, includeColumnNames, firstRow, firstColumn)
            table.ImportDataTable(data, true, 0, 0);

            // Add the populated table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with dynamically populated table saved as 'output.pdf'.");
    }
}