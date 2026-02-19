using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF/A file
        const string inputPath = "input.pdf";
        // Path where the regular PDF will be saved
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A document
            Document pdfDocument = new Document(inputPath);

            // Save it as a standard PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Conversion successful. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}