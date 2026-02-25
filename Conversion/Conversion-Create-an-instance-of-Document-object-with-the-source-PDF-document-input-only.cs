using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (input‑only)
        const string inputPath = "source.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The Document is now instantiated and loaded from the source PDF
            Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
            // Additional processing can be performed here
        }
    }
}