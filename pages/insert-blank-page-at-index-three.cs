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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a new blank page at index 3 (1‑based indexing).
            // The overload without a Page argument creates an empty page automatically.
            doc.Pages.Insert(3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted at index 3 and saved to '{outputPath}'.");
    }
}
