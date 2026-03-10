using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API, includes PptxSaveOptions

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize save options for PPTX format.
                // Passing a SaveOptions subclass is required when the target format is not PDF.
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as a PPTX file, preserving vector graphics.
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed successfully: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}