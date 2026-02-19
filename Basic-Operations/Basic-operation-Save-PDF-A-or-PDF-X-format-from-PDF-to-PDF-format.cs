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

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (PDF/A, PDF/X, or regular PDF)
            Document pdfDocument = new Document(inputPath);

            // Save the document as a standard PDF without any special options
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}