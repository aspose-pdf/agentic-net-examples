using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load an existing PDF document (input‑only) using a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Document is now loaded and ready for further processing
            Console.WriteLine($"Document loaded successfully. Page count: {doc.Pages.Count}");
        }
    }
}