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

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based; retrieve the first page
            Page firstPage = doc.Pages[1];

            // Change the page size to 500 × 700 points
            firstPage.SetPageSize(500, 700);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page resized and saved to '{outputPath}'.");
    }
}