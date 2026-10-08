using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Letter size in points (1 inch = 72 points)
        const double letterWidth  = 8.5 * 72; // 612 points
        const double letterHeight = 11  * 72; // 792 points

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Set the custom size for each page (pages are 1‑based in Aspose.Pdf)
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width  = letterWidth;
            page.PageInfo.Height = letterHeight;
            // Optional: ensure orientation is portrait
            page.PageInfo.IsLandscape = false;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Page size set to Letter portrait. Saved to '{outputPath}'.");
    }
}
