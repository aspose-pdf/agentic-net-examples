using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document; using ensures deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page firstPage = doc.Pages[1];

            // Change the first page size to 500 × 700 points
            firstPage.SetPageSize(500, 700);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page resized and saved to '{outputPath}'.");
    }
}