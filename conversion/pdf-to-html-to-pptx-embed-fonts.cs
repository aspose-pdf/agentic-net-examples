using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // for load options if needed (HtmlLoadOptions is in Aspose.Pdf)

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Intermediate HTML file
        const string htmlPath = "output.html";

        // Final PPTX file
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF to HTML with custom font saving
        // -------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // HtmlSaveOptions controls how fonts and resources are saved.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources (including fonts) into the single HTML file.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                // Save fonts as WOFF to ensure they are embedded and usable in browsers.
                FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF
            };

            pdfDoc.Save(htmlPath, htmlOpts);
        }

        // -------------------------------------------------
        // Step 2: Load the generated HTML and convert to PPTX
        // -------------------------------------------------
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // HtmlLoadOptions is required when loading an HTML file.
        HtmlLoadOptions htmlLoadOpts = new HtmlLoadOptions();

        using (Document htmlDoc = new Document(htmlPath, htmlLoadOpts))
        {
            // PptxSaveOptions allows control over glyph caching which helps
            // preserve the appearance of custom fonts in the resulting presentation.
            PptxSaveOptions pptxOpts = new PptxSaveOptions
            {
                // Cache glyphs to embed the visual representation of characters.
                CacheGlyphs = true
            };

            htmlDoc.Save(pptxPath, pptxOpts);
        }

        Console.WriteLine($"Conversion completed. HTML: {htmlPath}, PPTX: {pptxPath}");
    }
}