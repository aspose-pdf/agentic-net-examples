using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Output files
        const string htmlPath = "output.html";
        const string pptxPath = "output.pptx";

        // Verify input exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // ---------- Convert PDF → HTML ----------
                // Configure HTML save options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed fonts as WOFF to ensure they are available in the HTML output
                    FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF,
                    // Embed all resources (images, CSS, fonts) into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Optional: improve image handling
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML using the options above
                pdfDocument.Save(htmlPath, htmlOptions);
                Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");

                // ---------- Convert PDF → PPTX ----------
                // Configure PPTX save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    // CacheGlyphs forces glyphs (including font outlines) to be embedded,
                    // which results in a uniform appearance across machines.
                    CacheGlyphs = true
                };

                // Save the PDF as PPTX using the options above
                pdfDocument.Save(pptxPath, pptxOptions);
                Console.WriteLine($"PDF successfully converted to PPTX with embedded fonts: {pptxPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
