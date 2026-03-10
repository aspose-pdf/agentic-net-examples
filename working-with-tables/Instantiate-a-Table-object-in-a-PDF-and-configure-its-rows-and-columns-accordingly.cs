using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_example.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Instantiate a Table object
            Table table = new Table();

            // Optional visual configuration
            // NOTE: ColumnAdjustment.AutoFit is not available in the current Aspose.PDF version,
            // so this setting is omitted. The table will automatically size columns based on content.
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);        // Thin border around each cell
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);                // Padding inside cells
            table.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica")
            };

            // Define three column widths (as percentages of the total table width)
            table.ColumnWidths = "30 40 30";

            // ----- Header row -----
            Row headerRow = table.Rows.Add();          // Add a new row to the table
            headerRow.Cells.Add("Product");            // First column
            headerRow.Cells.Add("Quantity");           // Second column
            headerRow.Cells.Add("Price");              // Third column

            // ----- First data row -----
            Row dataRow1 = table.Rows.Add();
            dataRow1.Cells.Add("Widget A");
            dataRow1.Cells.Add("10");
            dataRow1.Cells.Add("$5.00");

            // ----- Second data row -----
            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add("Widget B");
            dataRow2.Cells.Add("7");
            dataRow2.Cells.Add("$7.50");

            // Add the configured table to the page's content
            page.Paragraphs.Add(table);

            // Save the PDF document (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
