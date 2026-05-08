using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at position 3.
            // PageCollection uses 1‑based indexing, so index 3 inserts after pages 1 and 2.
            doc.Pages.Insert(3);

            // Save the updated document (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted at index 3 and saved to '{outputPath}'.");
    }
}