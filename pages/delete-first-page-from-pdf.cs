using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

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

        // Document lifecycle must be wrapped in a using block (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            // Delete the first page; subsequent pages shift automatically.
            doc.Pages.Delete(1);

            // Save the modified PDF (rule: document-disposal-with-using ensures proper disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page removed. Result saved to '{outputPath}'.");
    }
}