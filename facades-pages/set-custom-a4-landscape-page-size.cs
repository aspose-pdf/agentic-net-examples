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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // A4 size in points: 595 (width) x 842 (height) for portrait.
        // For landscape we swap the dimensions.
        const double a4LandscapeWidth = 842;  // points
        const double a4LandscapeHeight = 595; // points

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Apply the custom size to every page.
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width = a4LandscapeWidth;
            page.PageInfo.Height = a4LandscapeHeight;
            // Optional: explicitly mark as landscape (not required if width > height).
            // page.PageInfo.IsLandscape = true;
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Widescreen PDF saved to '{outputPath}'.");
    }
}
