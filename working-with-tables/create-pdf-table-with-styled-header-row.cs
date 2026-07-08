using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // for TextState and FontStyles
using Aspose.Pdf.Drawing;       // for Color (Aspose.Pdf.Color)

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a visual table
            Table table = new Table
            {
                // Define three equal columns (widths in points)
                ColumnWidths = "150 150 150"
            };

            // ---------- Header row ----------
            // Add the header row to the table
            Row headerRow = table.Rows.Add();

            // Set background color for the entire header row
            headerRow.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Define default text style for all cells in this row (bold font)
            headerRow.DefaultCellTextState = new TextState
            {
                FontStyle = FontStyles.Bold
            };

            // Add header cells
            headerRow.Cells.Add("Product");
            headerRow.Cells.Add("Quantity");
            headerRow.Cells.Add("Price");

            // ---------- Sample data rows ----------
            Row dataRow1 = table.Rows.Add();
            dataRow1.Cells.Add("Widget A");
            dataRow1.Cells.Add("10");
            dataRow1.Cells.Add("$5.00");

            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add("Widget B");
            dataRow2.Cells.Add("7");
            dataRow2.Cells.Add("$8.50");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("styled_table.pdf");
        }

        Console.WriteLine("PDF with styled header row saved as 'styled_table.pdf'.");
    }
}
