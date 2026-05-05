using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF using the Document class (PdfFileEditor does not provide BindPdf/Close)
        Document pdfDoc = new Document(inputPath);

        // Example of using PdfFileEditor for an operation that works directly with file paths
        // (uncomment if needed)
        // PdfFileEditor editor = new PdfFileEditor();
        // editor.Extract(inputPath, "page1.pdf", new[] { 1 }); // extracts first page

        Console.WriteLine("PDF successfully loaded with Document class.");
    }
}
