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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the beginning (position 1, 1‑based indexing)
            Page newPage = doc.Pages.Insert(1);

            // Set custom dimensions: 200 × 300 points
            newPage.SetPageSize(200, 300);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted page saved to '{outputPath}'.");
    }
}