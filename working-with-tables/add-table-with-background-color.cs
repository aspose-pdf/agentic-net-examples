using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_gradient_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a simple table with two columns
            Table table = new Table
            {
                // Define column widths (percentage or absolute units)
                ColumnWidths = "200 200",
                // Position the table on the page using MarginInfo (left, right, top, bottom)
                Margin = new MarginInfo { Left = 50, Bottom = 700 }
            };

            // Create a linear gradient using GradientAxialShading (Aspose.Pdf.Drawing)
            var axialGradient = new GradientAxialShading(
                Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0), // start color (red)
                Aspose.Pdf.Color.FromRgb(0.0, 0.0, 1.0)  // end color (blue)
            )
            {
                Start = new Point(0, 0),   // start point (relative to the table)
                End = new Point(0, 200)    // end point (relative to the table)
            };

            // Apply the gradient to the table's background color
            table.BackgroundColor = new Color { PatternColorSpace = axialGradient };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A");
            data.Cells.Add("Cell B");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with gradient table: {outputPath}");
    }
}
