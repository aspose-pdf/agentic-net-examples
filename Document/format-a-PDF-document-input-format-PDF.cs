using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "formatted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Example formatting operations
                pdfDoc.Optimize();               // Linearize for faster web access
                pdfDoc.RemoveMetadata();         // Strip existing metadata
                pdfDoc.SetTitle(Path.GetFileNameWithoutExtension(outputPath)); // Set a new title

                // Save the formatted PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Formatted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}