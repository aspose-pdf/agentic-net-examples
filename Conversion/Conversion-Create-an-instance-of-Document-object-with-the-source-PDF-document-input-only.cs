using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (must exist)
        const string inputPdfPath = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document using the Document(string) constructor
        Document pdfDocument = new Document(inputPdfPath);

        // Example usage: display the number of pages in the loaded document
        Console.WriteLine($"PDF loaded successfully. Page count: {pdfDocument.Pages.Count}");

        // Optional: save the loaded document to a new file (demonstrates the document-save rule)
        const string outputPdfPath = "output.pdf";
        // Simple save without options
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"Document saved to '{outputPdfPath}'.");
    }
}