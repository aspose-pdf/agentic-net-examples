using System;
using System.IO;
using System.Linq;
using System.Globalization;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a table with three columns
            Table table = new Table
            {
                // Define column widths (absolute values in points)
                ColumnWidths = "100 100 100"
            };

            // Add a single row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Insert the table into the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Force layout by saving to a memory stream (no file is written)
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                // After layout the actual width of the table can be derived from the column widths
                double renderedWidth = GetRenderedTableWidth(table);
                Console.WriteLine($"Rendered table width: {renderedWidth}");
            }

            // Save the modified PDF (optional)
            doc.Save(outputPath);
        }
    }

    /// <summary>
    /// Calculates the rendered width of a table after layout.
    /// Aspose.Pdf.Table does not expose a Width property; the effective width is the sum of the column widths.
    /// </summary>
    /// <param name="table">The table whose width is required.</param>
    /// <returns>The total width in points.</returns>
    private static double GetRenderedTableWidth(Table table)
    {
        if (string.IsNullOrWhiteSpace(table.ColumnWidths))
            return 0.0;

        // ColumnWidths is a space‑separated list of absolute values (e.g., "100 100 100")
        // Parse each value using invariant culture to avoid locale issues.
        var widths = table.ColumnWidths
                         .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(s => double.Parse(s, CultureInfo.InvariantCulture));
        return widths.Sum();
    }
}
