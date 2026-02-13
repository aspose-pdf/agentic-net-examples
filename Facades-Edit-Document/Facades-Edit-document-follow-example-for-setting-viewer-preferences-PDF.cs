using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

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

            // NOTE: The ViewerPreferences API is not available in the current Aspose.Pdf version.
            // If you need to set viewer preferences, upgrade to a version that supports it or
            // manipulate the PDF catalog dictionary directly (advanced scenario).
            // For this example we simply load and save the document.

            // Save the (unchanged) PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
