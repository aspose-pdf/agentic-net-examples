using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example 1: Resize all pages to a standard A4 size
            foreach (Page page in doc.Pages)
            {
                // Page.Resize accepts a PageSize instance.
                // PageSize.A4 is a predefined A4 size (297 x 210 mm).
                page.Resize(PageSize.A4);
            }

            // Example 2: Resize all pages to a custom size (e.g., 8.5 x 11 inches)
            // Uncomment the following block if a custom size is required.
            /*
            const float widthInInches  = 8.5f;
            const float heightInInches = 11f;
            // Convert inches to points (1 inch = 72 points)
            float widthPoints  = widthInInches  * 72f;
            float heightPoints = heightInInches * 72f;
            PageSize customSize = new PageSize(widthPoints, heightPoints);

            foreach (Page page in doc.Pages)
            {
                page.Resize(customSize);
            }
            */

            // Save the modified document as PDF.
            // Since we are saving to a PDF file, no SaveOptions are required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF pages resized and saved to '{outputPath}'.");
    }
}