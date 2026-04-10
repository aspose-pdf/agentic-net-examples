using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        // Paths to custom font files that should be embedded in the PPTX
        string[] customFontFiles = { "fonts/CustomFont1.ttf", "fonts/CustomFont2.otf" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        foreach (var fontPath in customFontFiles)
        {
            if (!File.Exists(fontPath))
            {
                Console.Error.WriteLine($"Custom font not found: {fontPath}");
                return;
            }
        }

        try
        {
            // Register each custom font with the FontRepository using SimpleFontSubstitution.
            // This makes the fonts available during conversion and forces embedding.
            foreach (var fontPath in customFontFiles)
            {
                // Use the file name (without extension) as the font family name for substitution.
                string fontFamily = Path.GetFileNameWithoutExtension(fontPath);
                FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontFamily, fontPath));
            }

            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure PPTX save options. CacheGlyphs embeds font glyphs into the presentation.
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    CacheGlyphs = true
                };

                // Convert and save the PDF as a PPTX file.
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
