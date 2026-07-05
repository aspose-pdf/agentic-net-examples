using System;
using System.IO;                    // For file existence check and creating a sample PDF
using Aspose.Pdf;                    // Core Aspose.Pdf namespace (Document class)
using Aspose.Pdf.Facades;           // Facade API for annotation editing

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF
        const int    pageNumber = 1;             // target page (1‑based indexing)

        // Ensure a source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            // Create a one‑page blank PDF.
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Define the annotation rectangle in points (1 point = 1/72 inch).
        // Example: lower‑left corner at (100,500), width 200, height 100 points.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Red border colour.
        System.Drawing.Color red = System.Drawing.Color.Red;

        // 2 mm ≈ 5.67 points → round to 6 points for the border width.
        int borderWidth = 6;

        // Use PdfContentEditor (a Facades class) to add a square (rectangle) annotation.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath); // load the PDF
            // CreateSquareCircle(rect, contents, colour, square:true, page, borderWidth)
            editor.CreateSquareCircle(rect, "Red Rectangle", red, true, pageNumber, borderWidth);
            editor.Save(outputPath);   // persist changes
        }

        Console.WriteLine($"Red rectangle annotation added to page {pageNumber} and saved as '{outputPath}'.");
    }
}
