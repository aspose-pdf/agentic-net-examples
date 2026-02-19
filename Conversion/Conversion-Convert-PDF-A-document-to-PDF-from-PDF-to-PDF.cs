using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF/A document
        const string inputPath = "input.pdf";
        // Path where the converted PDF will be saved
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF/A document
        Document pdfDocument = new Document(inputPath);

        // Save it as a regular PDF
        pdfDocument.Save(outputPath); // uses the provided document-save rule

        Console.WriteLine($"Conversion completed. PDF saved to '{outputPath}'.");
    }
}