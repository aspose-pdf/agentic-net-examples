using System;
using System.Drawing;               // System.Drawing.Rectangle and System.Drawing.Color
using System.IO;                    // File.Exists, FileStream
using Aspose.Pdf;                    // Document class for creating a placeholder PDF
using Aspose.Pdf.Facades;           // PdfContentEditor for annotation handling

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple
        // one‑page PDF that we can later annotate. This removes the
        // FileNotFoundException that was raised in the original code.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a blank page (default size A4).
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Initialize the content editor and bind the source PDF.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Use fully‑qualified System.Drawing.Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a rectangle (square) annotation with a light‑gray fill and a 1 pt border.
            // The Color parameter must be System.Drawing.Color, so we qualify it as well.
            editor.CreateSquareCircle(
                annotRect,
                "Custom rectangle annotation",
                System.Drawing.Color.LightGray,   // fill colour
                true,                              // square shape (true = rectangle)
                1,                                 // page number (1‑based)
                1);                                // border width = 1 pt

            // Save the PDF with the new annotation.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation added. Output saved to '{outputPath}'.");
    }
}
