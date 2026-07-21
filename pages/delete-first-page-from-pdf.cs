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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page numbers are 1‑based; delete the first page
            doc.Pages.Delete(1);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page deleted. Result saved to '{outputPath}'.");
    }
}