using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure PPTX save options for fast rendering
                //   - CacheGlyphs reduces repeated glyph processing (better performance, higher memory)
                //   - Enable multithreading to process pages in parallel (instance property)
                var pptxOpts = new PptxSaveOptions
                {
                    CacheGlyphs = true,
                    IsMultiThreading = true
                };

                // Save the PDF as PPTX using the configured options
                pdfDoc.Save(outputPptxPath, pptxOpts);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}