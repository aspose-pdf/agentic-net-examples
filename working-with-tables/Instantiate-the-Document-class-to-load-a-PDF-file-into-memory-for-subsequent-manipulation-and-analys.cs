using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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

        // Load the PDF document into memory.
        // The Document object is wrapped in a using statement for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // At this point the PDF is loaded and ready for manipulation or analysis.
            // Example: output basic information about the document.
            Console.WriteLine($"Pages: {pdfDoc.Pages.Count}");
            Console.WriteLine($"Title: {pdfDoc.Info.Title}");
            Console.WriteLine($"Author: {pdfDoc.Info.Author}");

            // Additional processing can be performed here using pdfDoc...
        } // Document is automatically disposed here.
    }
}