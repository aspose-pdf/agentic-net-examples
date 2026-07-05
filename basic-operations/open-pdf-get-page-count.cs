using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document(pdfPath))
        {
            // Pages are 1‑based; retrieve the total number of pages
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Document contains {pageCount} page(s).");
        }
    }
}