using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF/A or PDF/X file
        string inputPath = "input.pdf";

        // Path where the regular PDF will be saved
        string outputPath = "output.pdf";

        // Ensure the source file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document (PDF/A or PDF/X)
            Document pdfDocument = new Document(inputPath);

            // Save the document as a standard PDF (no special options required)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}