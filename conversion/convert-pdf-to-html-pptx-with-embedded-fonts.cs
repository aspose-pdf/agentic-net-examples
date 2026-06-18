using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Desired output files
        const string htmlPath = "output.html";
        const string pptxPath = "output.pptx";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Convert PDF to HTML and embed fonts (WOFF)
        // -------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Embed fonts as WOFF files so they are available in the HTML
            htmlOptions.FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF;

            // Embed all resources (images, CSS, fonts) into a single HTML file
            htmlOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Save the HTML file with the specified options
            pdfDoc.Save(htmlPath, htmlOptions);
        }

        // -------------------------------------------------
        // 2. Convert PDF to PPTX and embed custom fonts
        // -------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Cache glyphs to ensure font information is embedded in the PPTX
            pptxOptions.CacheGlyphs = true;

            // Save the PPTX file with the specified options
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        Console.WriteLine("PDF successfully converted to HTML and PPTX with embedded fonts.");
    }
}