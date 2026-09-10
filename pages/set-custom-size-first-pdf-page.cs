using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom_size.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; get the first page
            Page firstPage = doc.Pages[1];

            // Change the page size to 500 × 700 points
            firstPage.SetPageSize(500, 700);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom first‑page size to '{outputPath}'.");
    }
}