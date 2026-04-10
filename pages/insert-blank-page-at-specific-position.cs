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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at index 3 (pages are 1‑based)
            doc.Pages.Insert(3);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted and saved to '{outputPath}'.");
    }
}