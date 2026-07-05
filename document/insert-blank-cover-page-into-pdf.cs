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
            // Insert an empty page at the very beginning (position 1, 1‑based indexing)
            doc.Pages.Insert(1);

            // Save the updated PDF with the new cover page
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank cover page added. Saved to '{outputPath}'.");
    }
}