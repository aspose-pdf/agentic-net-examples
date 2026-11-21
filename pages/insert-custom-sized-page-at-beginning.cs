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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a new empty page at the beginning (Aspose.Pdf uses 1‑based indexing)
            Page emptyPage = doc.Pages.Insert(1);
            // Set custom dimensions: 200 × 300 points
            emptyPage.PageInfo.Width = 200;
            emptyPage.PageInfo.Height = 300;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted empty page and saved to '{outputPath}'.");
    }
}