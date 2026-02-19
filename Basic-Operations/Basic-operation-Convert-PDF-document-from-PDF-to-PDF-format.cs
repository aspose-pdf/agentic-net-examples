using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Save the document back to PDF format (no conversion needed)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}