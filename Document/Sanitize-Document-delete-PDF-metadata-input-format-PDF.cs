using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        string inputPath = "input.pdf";
        string outputPath = "output_sanitized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Remove standard document information (Title, Author, etc.)
            pdfDocument.Info.Clear();

            // Remove custom metadata entries and XMP metadata
            pdfDocument.Metadata.Clear();

            // Save the sanitized PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}