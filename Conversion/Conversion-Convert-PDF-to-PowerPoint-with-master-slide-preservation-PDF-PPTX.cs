using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output PPTX file path
        const string outputPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Configure PPTX save options (default settings preserve master slide)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save the document as PPTX
            pdfDocument.Save(outputPath, pptxOptions);

            Console.WriteLine($"Conversion successful. PPTX saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}