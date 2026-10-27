using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "custom_sized.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Custom page dimensions (in points; 1 point = 1/72 inch)
        // Example: 8.5 x 11 inches => 612 x 792 points.
        // Here we use non‑standard dimensions: 500 x 700 points.
        const double customWidth  = 500.0;
        const double customHeight = 700.0;

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Iterate through all pages and set the custom size.
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width  = customWidth;
            page.PageInfo.Height = customHeight;
        }

        // Save the modified PDF with the new page dimensions.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved with custom page size to '{outputPath}'.");
    }
}
