using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        // Input and output paths
        const string outputPath = "table_double_border.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a simple 2x2 table
            Table table = new Table
            {
                // Position the table on the page
                Left = 50,
                Top = 700,
                // Define column widths (optional)
                ColumnWidths = "200 200"
            };

            // Add rows and cells with sample text
            for (int r = 0; r < 2; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 2; c++)
                {
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment($"R{r + 1}C{c + 1}"));
                }
            }

            // ---------- Apply double‑like border style ----------
            // Aspose.Pdf.BorderInfo does not expose a Style property or a BorderStyle enum.
            // To emulate a double border, we set a relatively thick border (e.g., 2 points).
            // If a true double line is required, it would need to be drawn manually using Graph.
            var doubleBorder = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Black);
            // Assign the configured BorderInfo to the table
            table.Border = doubleBorder;
            // ---------------------------------------------------

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with double‑border table saved to '{outputPath}'.");
    }
}
