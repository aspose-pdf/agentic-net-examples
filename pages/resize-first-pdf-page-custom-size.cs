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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count >= 1)
            {
                // Pages are 1‑based (global rule)
                Page firstPage = doc.Pages[1];

                // Set custom dimensions: 500 × 700 points
                firstPage.SetPageSize(500, 700);
            }

            // Save the modified PDF (basic save, PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page resized and saved to '{outputPath}'.");
    }
}