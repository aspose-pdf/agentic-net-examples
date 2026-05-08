using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF
        const string outputPath = "output.pdf";  // PDF with new first page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF, modify, and save – all within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the very beginning (position 1, because Aspose.Pdf uses 1‑based indexing)
            Page insertedPage = doc.Pages.Insert(1);

            // Set the custom dimensions: width = 200 points, height = 300 points
            insertedPage.SetPageSize(200, 300);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new first page: '{outputPath}'");
    }
}