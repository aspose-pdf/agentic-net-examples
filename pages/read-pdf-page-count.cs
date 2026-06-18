using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file whose page count we want to read
        const string inputPath = "sample.pdf";

        // Verify that the file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based; Count returns the total number of pages
            int pageCount = doc.Pages.Count;

            // Output the page count to the console
            Console.WriteLine($"Document contains {pageCount} page(s).");
        }
    }
}