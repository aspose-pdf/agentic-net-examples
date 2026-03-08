using System;
using System.IO;
using Aspose.Pdf;               // Document type
using Aspose.Pdf.Facades;      // Facade classes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into memory using a facade (PdfExtractor in this case)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file; the facade loads it and creates an in‑memory Document
            extractor.BindPdf(inputPath);

            // Access the loaded Document for further processing
            Document doc = extractor.Document;

            // Example operation: display the number of pages
            Console.WriteLine($"PDF loaded with {doc.Pages.Count} page(s).");

            // Additional processing can be performed on 'doc' here
        } // extractor (and the underlying Document) are disposed here
    }
}