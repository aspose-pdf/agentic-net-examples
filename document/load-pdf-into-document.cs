using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Path to the PDF file to be loaded
        const string inputPath = "sample.pdf";

        // Verify that the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a Document object.
        // The Document constructor that accepts a filename opens the file.
        // Wrapped in a using block to ensure deterministic disposal (resource leak prevention).
        using (Document doc = new Document(inputPath))
        {
            // At this point 'doc' is ready for further processing.
            Console.WriteLine($"PDF loaded successfully. Pages: {doc.Pages.Count}");
            // Example: you could now manipulate the document, e.g., add annotations, extract text, etc.
        }
    }
}