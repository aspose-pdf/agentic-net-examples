using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // For Aspose.Pdf.Rectangle (fully qualified when used)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with two columns
            Table table = new Table
            {
                // Define column widths (e.g., 150 points each)
                ColumnWidths = "150 150",
                // Optional: set table position on the page
                // Uncomment and adjust the rectangle as needed
                // Rectangle = new Aspose.Pdf.Rectangle(50, 750, 400, 650)
            };

            // ----- First row: merge the first two cells using ColSpan -----
            Row firstRow = table.Rows.Add();
            Cell mergedCell = firstRow.Cells.Add("Merged Cell");
            mergedCell.ColSpan = 2;               // Merge across two columns
            mergedCell.BackgroundColor = Aspose.Pdf.Color.LightGray; // visual cue

            // ----- Second row: regular two cells -----
            Row secondRow = table.Rows.Add();
            secondRow.Cells.Add("Cell 1");
            secondRow.Cells.Add("Cell 2");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}