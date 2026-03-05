using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination PPTX
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize save options for PowerPoint (PPTX) format
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX with selectable text (lifecycle: save)
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}