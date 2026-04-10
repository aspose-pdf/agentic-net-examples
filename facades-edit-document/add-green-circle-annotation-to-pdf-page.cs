using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Rectangle and System.Drawing.Color (used by PdfContentEditor)

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

        // Define the rectangle that will surround the diagram on page 6.
        // Adjust the coordinates (x, y, width, height) as needed.
        // Use System.Drawing.Rectangle because PdfContentEditor.CreateSquareCircle expects it.
        System.Drawing.Rectangle diagramRect = new System.Drawing.Rectangle(100, 500, 200, 300);

        // Use PdfContentEditor (Facades API) to add a circle annotation.
        // CreateSquareCircle parameters:
        //   rect          – annotation rectangle (System.Drawing.Rectangle)
        //   contents      – optional text (empty here)
        //   clr           – outline color (System.Drawing.Color)
        //   square        – false => circle
        //   page          – target page number (6, 1‑based)
        //   borderWidth   – thickness of the outline (e.g., 5)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            editor.CreateSquareCircle(diagramRect, "", System.Drawing.Color.Green, false, 6, 5);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added to page 6 and saved as '{outputPath}'.");
    }
}
