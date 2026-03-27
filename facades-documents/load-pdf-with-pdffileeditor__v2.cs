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

        // Load the PDF using the Document class (PdfFileEditor has no BindPdf method)
        Document pdfDoc = new Document(inputPath);

        Console.WriteLine("PDF file loaded successfully using Document class.");
    }
}
