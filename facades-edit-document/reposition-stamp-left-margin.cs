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

        // 10 mm expressed in points (1 pt = 1/72 inch, 1 inch = 25.4 mm)
        const double mmToPoints = 72.0 / 25.4; // ≈2.8346457
        double leftMarginPoints = 10 * mmToPoints; // 10 mm → points

        // Move the first stamp on page 4 to the left margin (10 mm from the left edge)
        // Stamp index is 1‑based; adjust if a different stamp is required.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.MoveStamp(pageNumber: 4, stampIndex: 1, x: leftMarginPoints, y: 0); // y = 0 keeps the stamp at the bottom; adjust if needed
        editor.Save(outputPath);

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}