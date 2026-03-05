using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the input file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document in a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example read‑only operation: output the number of pages
            // (Pages collection is 1‑based as per the global rule)
            Console.WriteLine($"Document loaded. Page count: {doc.Pages.Count}");
        }
    }
}