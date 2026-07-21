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

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at position 3.
            // Page numbers are 1‑based, so index 3 inserts after pages 1 and 2.
            doc.Pages.Insert(3);

            // Save the modified document (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted at index 3 and saved to '{outputPath}'.");
    }
}