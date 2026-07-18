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

        // Load the PDF using the Document class (PdfFileEditor does not support BindPdf or expose a Document).
        Document pdfDoc = new Document(inputPath);

        // Example usage: read information from the loaded document.
        int pageCount = pdfDoc.Pages.Count;
        Console.WriteLine($"Loaded PDF has {pageCount} pages.");

        // If you need to save (potentially modified) PDF, you can call:
        // pdfDoc.Save("output.pdf");
    }
}
