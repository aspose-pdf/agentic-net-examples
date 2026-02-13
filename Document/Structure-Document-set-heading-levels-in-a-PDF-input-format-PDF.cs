using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main(string[] args)
    {
        // Generic input and output file names – works on any OS
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists before trying to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // NOTE: The AutoTaggingSettings API is not available in the current
            // Aspose.Pdf version used for this project, therefore it has been
            // removed. If you need auto‑tagging, upgrade to a version that
            // supports Document.AutoTaggingSettings or use the dedicated
            // Aspose.Pdf.Tagging namespace (when available).

            // Save the (unchanged) PDF to the output path
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Provide a clear, user‑friendly error message
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
