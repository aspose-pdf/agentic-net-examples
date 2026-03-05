using System;
using System.IO;
using Aspose.Pdf; // Core PDF API and save options are in this namespace

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output PPTX file path
        const string pptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize save options for PPTX conversion.
                // PptxSaveOptions inherits from UnifiedSaveOptions and controls the conversion process.
                // The default settings embed fonts; you can tweak CacheGlyphs if needed.
                var saveOptions = new PptxSaveOptions
                {
                    // Example: enable glyph caching to improve performance (optional)
                    CacheGlyphs = true
                };

                // Save the PDF as PPTX using the specified options (lifecycle: save)
                pdfDocument.Save(pptxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}