using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the total number of pages (1‑based collection)
            int pageCount = doc.Pages.Count;

            // Output the page count to the console
            Console.WriteLine($"Document contains {pageCount} pages.");
        }
    }
}