using System;
using System.IO;
using Aspose.Pdf; // Core PDF API and save options are in this namespace

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options.
                // CacheGlyphs = true forces the converter to cache font glyphs,
                // which results in fonts being embedded in the generated PPTX.
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    CacheGlyphs = true
                };

                // Save the PDF as PPTX using the specified options (lifecycle: save)
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}' created.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}