using System;
using System.IO;
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

        // 10 mm expressed in points (1 inch = 72 points, 1 mm = 72/25.4 points)
        const double mmToPoints = 72.0 / 25.4;
        double leftMarginX = 10 * mmToPoints; // 10 mm from the left edge

        // Move the first stamp (index 1) on page 4.
        // PdfContentEditor uses 1‑based page indexing.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            editor.MoveStamp(pageNumber: 4, stampIndex: 1, x: leftMarginX, y: 0);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}