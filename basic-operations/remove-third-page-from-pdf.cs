using System;
using System.IO;
using Aspose.Pdf;               // Core API namespace

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

        // Load the PDF, delete page 3, and save the result.
        // Document is wrapped in a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // PageCollection uses 1‑based indexing; delete the third page.
            doc.Pages.Delete(3);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Third page removed. Saved to '{outputPath}'.");
    }
}