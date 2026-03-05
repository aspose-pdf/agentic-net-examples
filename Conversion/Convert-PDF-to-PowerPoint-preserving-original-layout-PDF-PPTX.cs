using System;
using System.IO;
using Aspose.Pdf; // PptxSaveOptions resides in this namespace

class Program
{
    static void Main()
    {
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
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize save options for PowerPoint (PPTX) format
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX while preserving the original layout
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