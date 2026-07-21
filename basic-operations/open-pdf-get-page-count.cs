using System;
using System.IO;
using Aspose.Pdf;

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

        // Open the PDF using the Document constructor.
        // The using block ensures the document is disposed properly.
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; Count gives the total number of pages.
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document contains {pageCount} page(s).");
        }
    }
}