using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (input‑only)
        string inputPath = "input.pdf";

        // Path where the document will be saved (can be the same or a different file)
        string outputPath = "output.pdf";

        // Verify that the input file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document by creating a Document instance with the file name
            using (Document pdfDocument = new Document(inputPath))
            {
                // Save the loaded document (uses the required document-save rule)
                pdfDocument.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully loaded and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}