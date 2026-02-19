using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PowerPoint file path
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure PPTX save options to generate image‑only slides
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true,      // Each slide will contain a single image of the PDF page
                SeparateImages = true       // Keep images separate from other graphics (optional)
            };

            // Save the PDF as a PowerPoint presentation using the configured options
            pdfDocument.Save(outputPptxPath, pptxOptions);

            Console.WriteLine($"Conversion successful. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}