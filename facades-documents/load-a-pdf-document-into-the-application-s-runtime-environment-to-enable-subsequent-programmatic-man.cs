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

        // Load the PDF document. Document implements IDisposable.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Retrieve the number of pages.
            int pageCount = pdfDoc.Pages.Count;
            Console.WriteLine($"Loaded PDF with {pageCount} pages.");
        }
    }
}