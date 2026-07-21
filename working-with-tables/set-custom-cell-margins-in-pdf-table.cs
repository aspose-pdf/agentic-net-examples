using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_margins.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a table, set column widths and optional default cell padding
            Table table = new Table
            {
                ColumnWidths = "150 150 150",
                // Optional: set a default padding for all cells in the table
                DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add("Cell with custom margins");

            // Configure the cell's margins using a MarginInfo instance
            // Parameters: left, bottom, right, top
            cell.Margin = new MarginInfo(12, 6, 12, 6);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}