using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a new empty page at the very beginning (1‑based index)
            Page newPage = doc.Pages.Insert(1);

            // Set the custom dimensions: 200 × 300 points
            newPage.SetPageSize(200, 300);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted page saved to '{outputPath}'.");
    }
}