using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) or default
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        // Output PPTX path (second argument) or default
        string outputPath = args.Length > 1 ? args[1] : "output.pptx";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Save as PowerPoint. The .pptx extension tells Aspose.Pdf to use PPTX format.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Conversion completed successfully. PPTX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}