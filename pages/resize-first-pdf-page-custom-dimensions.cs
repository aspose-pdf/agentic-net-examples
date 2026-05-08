using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (global rule)
            Page firstPage = doc.Pages[1];

            // Change the size of the first page to 500 × 700 points
            firstPage.SetPageSize(500, 700);

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page resized and saved to '{outputPath}'.");
    }
}