using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF (input‑only)
        const string inputPath = "source.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document constructor.
        // The using block ensures deterministic disposal of the Document object.
        using (Document doc = new Document(inputPath))
        {
            // Example: output the number of pages in the loaded document
            Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
            
            // Additional processing can be performed here...
        }
    }
}