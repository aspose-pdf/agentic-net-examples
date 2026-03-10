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

        // Load the PDF document (input‑only) using the Document constructor
        using (Document pdfDoc = new Document(inputPath))
        {
            Console.WriteLine($"PDF loaded from: {inputPath}");
            Console.WriteLine($"Number of pages: {pdfDoc.Pages.Count}");
        }
    }
}