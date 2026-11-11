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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Letter size in points (1 inch = 72 points)
        const double letterWidth = 8.5 * 72; // 612 points
        const double letterHeight = 11 * 72; // 792 points

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Iterate through all pages and set the custom Letter portrait size
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width = (float)letterWidth;
            page.PageInfo.Height = (float)letterHeight;
            // Ensure orientation is portrait (optional, Aspose infers from width/height)
            page.PageInfo.IsLandscape = false;
        }

        // Save the updated PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"All pages resized to Letter portrait and saved to '{outputPath}'.");
    }
}
