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

        // Convert 10 mm to points (1 inch = 72 points, 1 inch = 25.4 mm)
        double mm      = 10.0;
        double points  = mm * 72.0 / 25.4; // ≈ 28.3465 points

        // Initialize the content editor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the source PDF
        editor.BindPdf(inputPath);

        // Move the first stamp (index 1) on page 4 to the left margin +10 mm.
        // Y‑coordinate is set to 0 (bottom of the page); adjust if needed.
        editor.MoveStamp(4, 1, points, 0);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}