using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where the PPTX will be saved.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "input.pdf");
        // Output PPTX file.
        string pptxPath = Path.Combine(dataDir, "output.pptx");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create PPTX save options.
            // Setting CacheGlyphs to true forces the converter to cache font glyphs,
            // which results in the glyph shapes being embedded in the PPTX.
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                CacheGlyphs = true
            };

            // Save the PDF as a PPTX presentation using the specified options.
            pdfDocument.Save(pptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with embedded fonts: {pptxPath}");
    }
}