using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) or default value
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        // Output PPTX path (second argument) or default value
        string outputPath = args.Length > 1 ? args[1] : "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Save the document as PPTX, preserving vector graphics
            pdfDocument.Save(outputPath, SaveFormat.Pptx);

            Console.WriteLine($"Conversion successful. PPTX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}