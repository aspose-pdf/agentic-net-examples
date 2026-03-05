using System;
using System.IO;
using Aspose.Pdf;   // Document and DocumentFactory are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a DocumentFactory instance (no disposable resources needed)
        DocumentFactory factory = new DocumentFactory();

        // Load the PDF using the factory; wrap the Document in a using block for proper disposal
        using (Document doc = factory.CreateDocument(inputPath))
        {
            // The document is now loaded and ready for read‑only operations
            Console.WriteLine($"Document loaded. Page count: {doc.Pages.Count}");
        }
    }
}