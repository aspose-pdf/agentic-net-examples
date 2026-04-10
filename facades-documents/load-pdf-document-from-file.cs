using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF using the Document class (PdfFileEditor does not provide BindPdf/Close)
        Document pdfDoc = new Document(inputPath);

        // The PDF is now loaded and ready for manipulation.
        Console.WriteLine("PDF loaded successfully using Document class.");
    }
}