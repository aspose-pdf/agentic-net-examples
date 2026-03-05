using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and PptxSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PowerPoint file path (PPTX)
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create/load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize save options for PPTX output (must pass explicit SaveOptions)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as a PowerPoint file with selectable text
                pdfDocument.Save(outputPptxPath, pptxOptions); // lifecycle: save
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}