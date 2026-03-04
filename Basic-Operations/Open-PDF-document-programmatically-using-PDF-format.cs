using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file to open
        const string inputPath = "sample.pdf";

        // Verify that the file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document using a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example operation: display basic information about the opened PDF
            Console.WriteLine($"Opened PDF: {Path.GetFileName(inputPath)}");
            Console.WriteLine($"Number of pages: {pdfDoc.Pages.Count}");
            Console.WriteLine($"Author: {pdfDoc.Info.Author}");
            Console.WriteLine($"Title: {pdfDoc.Info.Title}");
        }

        // At this point the Document has been disposed automatically
    }
}