using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (required for non‑PDF output)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as a PowerPoint file with chart conversion
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion succeeded: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}