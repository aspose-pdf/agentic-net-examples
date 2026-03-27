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

        // Load the PDF using the Document class (PdfFileEditor does not have BindPdf)
        Document pdfDoc = new Document(inputPath);

        Console.WriteLine("PDF loaded successfully with Aspose.Pdf.Document.");
    }
}
