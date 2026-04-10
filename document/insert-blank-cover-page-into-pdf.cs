using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_cover.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at the very beginning (position 1, 1‑based indexing)
            doc.Pages.Insert(1);

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank cover page inserted and saved to '{outputPath}'.");
    }
}