using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; Pages.Count returns the total number of pages
            int pageCount = doc.Pages.Count;

            // Output the page count to the console
            Console.WriteLine($"Document contains {pageCount} page(s).");
        }
    }
}