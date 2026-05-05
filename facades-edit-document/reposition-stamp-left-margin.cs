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
        double leftMarginPoints = 10.0 * mmToPoints; // ≈ 28.3465 points

        // Move the first stamp on page 4 to the left margin (10 mm from the left edge)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        // stampIndex is 1‑based; assuming we want to move the first stamp on the page
        editor.MoveStamp(pageNumber: 4, stampIndex: 1, x: leftMarginPoints, y: 0);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}