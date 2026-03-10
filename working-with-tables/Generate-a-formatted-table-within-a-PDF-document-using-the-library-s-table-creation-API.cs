using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document (lifecycle: create)
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table (BaseParagraph)
            Table table = new Table();

            // Optional: set visual appearance
            table.BackgroundColor = Color.LightGray;
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };
            // Optional: define column widths (comma‑separated string)
            table.ColumnWidths = "100 150 200";

            // Prepare sample data in a DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Product");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");
            dt.Rows.Add("Apple", "10", "$1.00");
            dt.Rows.Add("Banana", "5", "$0.50");
            dt.Rows.Add("Cherry", "20", "$2.00");

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, import column names as first row, start row, start column)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted table saved to '{outputPath}'.");
    }
}