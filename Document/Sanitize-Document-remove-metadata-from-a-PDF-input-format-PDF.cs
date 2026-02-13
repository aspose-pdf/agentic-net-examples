using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Remove all metadata (predefined and custom)
        pdfDocument.Info.Clear();

        // Save the sanitized PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
    }
}