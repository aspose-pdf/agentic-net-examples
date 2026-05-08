using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facade API for editing PDF content

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

        // Initialize the PDF content editor (lifecycle: create)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document (lifecycle: load)
            editor.BindPdf(inputPath);

            // Define the rectangle that will bound the circle.
            // Adjust the coordinates (x, y, width, height) to surround the diagram on page 6.
            // System.Drawing.Rectangle expects (x, y, width, height).
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 200);

            // Add a circle annotation:
            //   contents   : empty string (no popup text)
            //   color      : green outline
            //   isSquare   : false (circle)
            //   pageNumber : 6 (Aspose.Pdf uses 1‑based indexing)
            //   borderWidth: 5 (thick outline)
            editor.CreateSquareCircle(
                rect,
                string.Empty,
                System.Drawing.Color.Green,
                false,
                6,
                5);

            // Save the modified PDF (lifecycle: save)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added to page 6 and saved as '{outputPath}'.");
    }
}
