using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (a copy of the input)
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document using the built‑in constructor
        Document pdfDocument = new Document(inputPath);

        // Example operation: display the number of pages
        Console.WriteLine($"Document loaded. Page count: {pdfDocument.Pages.Count}");

        // Save the document (simple save without options)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Document saved to: {outputPath}");
    }
}