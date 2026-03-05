using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions

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
            // Load the PDF document (lifecycle rule: load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (default settings generate notes pages where possible)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the document as PPTX (lifecycle rule: save with SaveOptions)
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed successfully. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}