using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be loaded
        const string inputPath = "sample.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into memory using a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The document is now loaded and ready for further processing
            Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");

            // TODO: Add further processing logic here (e.g., text extraction, manipulation, etc.)
        }
    }
}