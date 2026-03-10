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

        // Load the PDF document into memory. The Document is wrapped in a using block for proper disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // The document is now loaded and can be processed further.
            Console.WriteLine($"PDF loaded successfully. Page count: {pdfDoc.Pages.Count}");
            // Add further processing logic here.
        }
    }
}