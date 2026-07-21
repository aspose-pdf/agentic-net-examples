using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // Source PDF (will be created inline)
        const string outputPath = "output.pdf"; // Destination PDF

        // ---------------------------------------------------------------------
        // Create a minimal PDF file so that the example can run in a sandbox
        // where no external files exist. The PDF contains a single blank page.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add();
            seed.Save(inputPath);
        }

        // Define the annotation rectangle (coordinates in points).
        // System.Drawing.Rectangle constructor expects X, Y, Width, Height.
        // Lower‑left corner at (100, 500), width 200, height 100.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

        int pageNumber = 1;          // Target page (1‑based indexing)
        int borderWidth = 6;         // Approx. 2 mm (2 mm ≈ 5.7 points, rounded to 6)

        // Use the Facade to bind the PDF, add a red rectangle annotation, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // CreateSquareCircle creates a square (true) or circle (false) annotation.
            // Parameters: rectangle, contents, color, square flag, page, border width.
            editor.CreateSquareCircle(rect, string.Empty, System.Drawing.Color.Red, true, pageNumber, borderWidth);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Red rectangle annotation added to page {pageNumber} and saved as '{outputPath}'.");
    }
}
