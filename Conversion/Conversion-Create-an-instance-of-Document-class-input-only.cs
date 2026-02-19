using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the existing PDF file (input‑only)
        const string inputPath = "input.pdf";
        // Path where the document will be saved (output)
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the input‑only constructor
            Document pdfDocument = new Document(inputPath);

            // Example operation: display the number of pages
            Console.WriteLine($"Loaded PDF with {pdfDocument.Pages.Count} page(s).");

            // Save the document to the specified output path
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Document saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}