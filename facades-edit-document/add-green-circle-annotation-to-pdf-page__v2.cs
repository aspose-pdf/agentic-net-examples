using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Bind the PDF, add a green circle annotation on page 6, then save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // System.Drawing.Rectangle that encloses the diagram (example coordinates).
            // Parameters: x, y, width, height.
            System.Drawing.Rectangle diagramRect = new System.Drawing.Rectangle(100, 400, 300, 200);

            // Create a circle (square = false) with thick green outline (borderWidth = 5).
            // Contents can be any descriptive text.
            editor.CreateSquareCircle(
                diagramRect,               // annotation rectangle (System.Drawing.Rectangle)
                "Diagram",                // contents (tooltip)
                System.Drawing.Color.Green, // outline color (System.Drawing.Color)
                false,                     // false => circle
                6,                         // target page (1‑based)
                5                          // border width (thick)
            );

            // Save the modified document.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}
