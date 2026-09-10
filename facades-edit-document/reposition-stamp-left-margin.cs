using System;
using System.IO;
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

        // 10 mm expressed in points (1 pt = 1/72 inch, 1 inch = 25.4 mm)
        const double mmToPoints = 72.0 / 25.4;
        double leftMarginX = 10 * mmToPoints; // ≈ 28.3465 points

        // Move the first stamp (index 1) on page 4 to the left margin +10 mm.
        // Y coordinate is set to 0 (bottom of the page) – adjust if needed.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            editor.MoveStamp(pageNumber: 4, stampIndex: 1, x: leftMarginX, y: 0);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}