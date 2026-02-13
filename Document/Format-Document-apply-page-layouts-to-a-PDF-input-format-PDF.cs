using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
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

            // Apply the desired page layout (e.g., TwoColumnLeft)
            pdfDocument.PageLayout = PageLayout.TwoColumnLeft;

            // Save the modified PDF (uses the document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF saved with new layout to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}