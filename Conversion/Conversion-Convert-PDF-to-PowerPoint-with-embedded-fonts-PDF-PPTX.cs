using System;
using System.IO;
using Aspose.Pdf;               // Core API and PptxSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF file
        const string outputPpt = "output.pptx";    // destination PowerPoint file

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure PPTX save options; enable glyph caching to ensure fonts are embedded
            PptxSaveOptions pptxOpts = new PptxSaveOptions
            {
                CacheGlyphs = true   // embeds font glyphs in the resulting PPTX
            };

            // Save the PDF as PPTX using the explicit save options (required for non‑PDF formats)
            pdfDoc.Save(outputPpt, pptxOpts);
        }

        Console.WriteLine($"Conversion completed: '{outputPpt}'");
    }
}