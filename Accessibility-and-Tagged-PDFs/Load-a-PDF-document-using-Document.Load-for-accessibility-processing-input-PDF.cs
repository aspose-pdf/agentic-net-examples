using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input PDF file
        const string inputPath = "input.pdf";

        // Optional path for the output PDF (demonstrates saving after loading)
        const string outputPath = "output.pdf";

        // Verify that the input file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document from the specified file
            Document pdfDocument = new Document(inputPath);

            // Example operation: display the number of pages (accessibility processing can be added here)
            Console.WriteLine($"PDF loaded successfully. Page count: {pdfDocument.Pages.Count}");

            // Save the document to a new file (uses the provided document-save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Document saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}