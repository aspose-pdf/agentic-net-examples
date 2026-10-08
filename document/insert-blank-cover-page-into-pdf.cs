using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
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
            // Insert a new blank page at the very beginning (1‑based index).
            // The overload without a Page argument creates an empty page automatically.
            doc.Pages.Insert(1);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank cover page inserted. Saved to '{outputPath}'.");
    }
}
