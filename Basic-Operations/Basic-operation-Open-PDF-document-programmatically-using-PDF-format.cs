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

        // Open the PDF document; the using block ensures proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example operations: display page count and title metadata
            Console.WriteLine($"Pages: {pdfDoc.Pages.Count}");
            Console.WriteLine($"Title: {pdfDoc.Info.Title}");
        }
    }
}