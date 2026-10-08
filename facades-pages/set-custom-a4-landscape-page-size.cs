using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_widescreen.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // A4 size in points: 595 (width) x 842 (height). Landscape swaps them.
        const double a4LandscapeWidth = 842;  // points
        const double a4LandscapeHeight = 595; // points

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Set each page to the custom A4 landscape dimensions.
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width = a4LandscapeWidth;
            page.PageInfo.Height = a4LandscapeHeight;
            // Optional: mark the page as landscape (not required for size change).
            page.PageInfo.IsLandscape = true;
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Widescreen PDF saved to '{outputPath}'.");
    }
}
