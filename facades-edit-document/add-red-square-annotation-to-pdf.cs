using System;
using System.IO;
using System.Drawing;               // For System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;                   // Core PDF classes
using Aspose.Pdf.Facades;           // Facade classes for annotation handling

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to add a square annotation with a red border
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) in points
            // System.Drawing.Rectangle is required by CreateSquareCircle
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a square annotation:
            //   - contents: optional text shown when the annotation is selected
            //   - clr:       border color – bright red (255,0,0)
            //   - square:    true  => square shape (false would create a circle)
            //   - page:      1     => first page (Aspose.Pdf uses 1‑based page indexing)
            //   - borderWidth: 2   => thickness of the border
            editor.CreateSquareCircle(
                annotRect,
                "Highlighted area",
                System.Drawing.Color.FromArgb(255, 0, 0), // Bright red border color
                true,                      // Square shape
                1,                         // Page number (1‑based)
                2);                        // Border width

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
