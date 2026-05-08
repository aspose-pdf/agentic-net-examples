using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to add a square annotation with custom border thickness and color.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Define the area to highlight on the page (x, y, width, height).
            // Fully qualify System.Drawing.Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle.
            var highlightRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a square annotation:
            // - contents: description shown when the annotation is selected.
            // - color: border color (red in this example).
            // - square: true (square shape; false would create a circle).
            // - page: 1 (Aspose.Pdf uses 1‑based page indexing).
            // - borderWidth: custom thickness of the border (5 points here).
            editor.CreateSquareCircle(
                highlightRect,
                "Important Section",
                System.Drawing.Color.Red,
                true,
                1,
                5);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}