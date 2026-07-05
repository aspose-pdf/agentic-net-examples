using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output paths
        const string outputHtml = "output.html";
        const string outputPptx = "output.pptx";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF to HTML with embedded fonts
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize HtmlSaveOptions
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

            // Embed fonts directly into the HTML as WOFF files
            htmlOpts.FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF;

            // Embed all resources (images, CSS, fonts) into a single HTML file
            htmlOpts.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Save the HTML file using the options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        // -------------------------------------------------
        // Step 2: Convert PDF to PPTX and embed fonts
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize PptxSaveOptions
            PptxSaveOptions pptxOpts = new PptxSaveOptions();

            // Enable glyph caching to ensure font data is embedded in the PPTX
            pptxOpts.CacheGlyphs = true;

            // Save the PPTX file using the options
            pdfDoc.Save(outputPptx, pptxOpts);
        }

        Console.WriteLine("PDF successfully converted to HTML and PPTX with embedded fonts.");
    }
}