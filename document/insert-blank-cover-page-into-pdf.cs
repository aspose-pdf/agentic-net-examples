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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the very beginning.
            // PageCollection uses 1‑based indexing, so position 1 inserts before the current first page.
            doc.Pages.Insert(1);

            // Save the updated PDF with the new cover page.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank cover page inserted and saved to '{outputPath}'.");
    }
}