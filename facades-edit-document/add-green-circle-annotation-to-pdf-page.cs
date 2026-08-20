using System;
using System.Drawing;               // required for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal PDF with at least two pages so the example can run in
        // an isolated sandbox where no external files exist.
        // ---------------------------------------------------------------------
        using (var placeholder = new Document())
        {
            // Page 1 (can be empty)
            placeholder.Pages.Add();
            // Page 2 – the page on which we will place the circle annotation
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Facade for annotation manipulation
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Define the annotation rectangle (x, y, width, height) using System.Drawing.Rectangle
                // The rectangle is expressed in points; adjust as needed.
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 100, 100);

                // Create a circle annotation on page 2:
                // - contents: optional text inside the annotation (empty here)
                // - color: green fill (System.Drawing.Color)
                // - isSquare: false (creates a circle)
                // - pageNumber: 2 (Aspose.Pdf uses 1‑based indexing)
                // - borderWidth: 3 points
                editor.CreateSquareCircle(rect, string.Empty, System.Drawing.Color.Green, false, 2, 3);

                // Save the modified PDF (lifecycle: save)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}
