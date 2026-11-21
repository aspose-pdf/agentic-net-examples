using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document class (the modern API)
        Document pdfDocument = new Document(inputPath);

        // US Letter size in points (1 point = 1/72 inch)
        const double letterWidth = 612;   // 8.5 inches * 72
        const double letterHeight = 792;  // 11 inches * 72

        // Resize pages 3 through 6 (pages are 1‑based in Aspose.Pdf)
        for (int pageNumber = 3; pageNumber <= 6; pageNumber++)
        {
            // Guard against documents with fewer pages than expected
            if (pageNumber > pdfDocument.Pages.Count)
                break;

            Page page = pdfDocument.Pages[pageNumber];
            page.PageInfo.Width = letterWidth;
            page.PageInfo.Height = letterHeight;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Pages 3‑6 resized to Letter and saved as '{outputPath}'.");
    }
}
