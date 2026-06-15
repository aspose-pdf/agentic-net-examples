using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the Document class (PdfFileEditor does not support BindPdf, Document, or Close).
        Document pdfDoc = new Document(inputPath);
        int pageCount = pdfDoc?.Pages?.Count ?? 0;
        Console.WriteLine($"Loaded PDF with {pageCount} pages.");
    }
}
