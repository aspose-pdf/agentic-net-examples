using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output PPTX file path
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document, convert it to PPTX and embed fonts
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Enable glyph caching – this forces font glyphs to be embedded
                CacheGlyphs = true
            };

            // Save the document as PPTX with the specified options
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with embedded fonts: {pptxPath}");
    }
}
