using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";

        // Output PDF file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Save the document as PDF (no additional options required)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF successfully saved to: {outputPath}");
    }
}