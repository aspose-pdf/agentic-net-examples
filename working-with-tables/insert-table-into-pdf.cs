using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // Required for Position
using Aspose.Pdf.Annotations;   // Kept for completeness

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path, target page number (1‑based) and insertion coordinates
        const string inputPath   = "input.pdf";
        const string outputPath  = "output.pdf";
        const int    targetPage  = 2;          // Insert table on page 2
        const double insertX      = 100;      // X coordinate (points) from left
        const double insertY      = 500;      // Y coordinate (points) from bottom

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page exists (pages are 1‑based)
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {targetPage}");
                return;
            }

            Page page = doc.Pages[targetPage];

            // ------------------------------------------------------------
            // Construct a visual Table
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Use Margin to position the table (Aspose.Pdf.Table does not expose a Position property)
                Margin = new MarginInfo(insertX, insertY, 0, 0),

                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (space‑separated string)
            table.ColumnWidths = "150 150";

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Price");

            // Mark the first row as a repeating header (Aspose.Pdf.Table uses RepeatingRowsCount)
            table.RepeatingRowsCount = 1;

            // Data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("$25.00");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("$40.00");

            // ------------------------------------------------------------
            // Insert the table into the page content
            // ------------------------------------------------------------
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
