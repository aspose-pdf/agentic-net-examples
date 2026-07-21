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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor facade and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // 10 mm expressed in points (1 inch = 72 points, 1 mm ≈ 2.8346 points)
            double leftMarginPoints = 10.0 * 72.0 / 25.4; // ≈ 28.3465 points

            // Move the first stamp (index 1) on page 4 to the new X coordinate.
            // Y coordinate is kept unchanged by passing the current Y value (0 here as placeholder).
            editor.MoveStamp(pageNumber: 4, stampIndex: 1, x: leftMarginPoints, y: 0);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}