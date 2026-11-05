using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the PDF file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a Document object inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // At this point the PDF is loaded and ready for further processing
            Console.WriteLine($"PDF loaded successfully. Pages: {doc.Pages.Count}");
            // Example: access document metadata
            Console.WriteLine($"Title: {doc.Info.Title}");
            Console.WriteLine($"Author: {doc.Info.Author}");
        } // Document is automatically disposed here
    }
}