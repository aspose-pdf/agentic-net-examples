using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // destination PDF
        const string creator    = "My Custom Creator";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize PdfFileInfo facade with the source PDF
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Set the custom Creator metadata
                pdfInfo.Creator = creator;

                // Persist the changes to a new file
                bool saved = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(saved
                    ? $"Creator set and saved to '{outputPath}'."
                    : "Failed to save the updated PDF.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}