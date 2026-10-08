using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be loaded
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a Document object.
        // The Document is wrapped in a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // At this point the PDF is loaded and ready for further processing.
            // Example: output the number of pages in the loaded document.
            Console.WriteLine($"Loaded PDF with {doc.Pages.Count} page(s).");
        }
    }
}