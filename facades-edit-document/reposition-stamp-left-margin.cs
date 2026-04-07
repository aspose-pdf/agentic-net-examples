using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists before attempting to bind it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPath}' was not found.");
            return;
        }

        // Use a using‑statement to ensure the editor is disposed correctly.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF into the content editor.
            editor.BindPdf(inputPath);

            // 10 mm expressed in points (1 mm = 72 pt / 25.4).
            double leftMarginPoints = 10.0 * 72.0 / 25.4;

            // Move the first stamp on page 4 so its left edge aligns with the left margin (10 mm from the edge).
            // Stamp index is 1‑based; Y coordinate is kept at 0 (bottom) for this example.
            editor.MoveStamp(4, 1, leftMarginPoints, 0.0);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}
