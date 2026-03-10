using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document; the using block ensures proper disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example operation: output the number of pages.
            Console.WriteLine($"PDF loaded successfully. Page count: {pdfDoc.Pages.Count}");
        }
    }
}