using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the content editor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // 10 mm expressed in points (1 inch = 72 points, 1 mm = 72/25.4 points)
        double leftMarginPoints = 10.0 * 72.0 / 25.4;

        // Move the first stamp on page 4 to the new X position; Y is set to 0 (bottom)
        editor.MoveStamp(4, 1, leftMarginPoints, 0);

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}
