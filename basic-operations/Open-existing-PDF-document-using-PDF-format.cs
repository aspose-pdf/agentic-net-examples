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

        // Open the existing PDF document (PDF format) using the Document constructor.
        // The Document is wrapped in a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example operation: output the number of pages in the opened document.
            Console.WriteLine($"Opened '{inputPath}'. Page count: {pdfDoc.Pages.Count}");
        }
    }
}