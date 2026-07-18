using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string pdfPath = "input.pdf";

        // Output HTML path
        const string htmlPath = "output.html";

        // Output PPTX path
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // ---------- Convert PDF to HTML ----------
            // HtmlSaveOptions allows embedding fonts directly into the HTML.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Embed all resources (fonts, images, CSS) into the single HTML file.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                // Ensure fonts are saved as TrueType files and embedded.
                FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsTTF
            };

            // Save the PDF as HTML with the specified options.
            pdfDoc.Save(htmlPath, htmlOptions);
            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");

            // ---------- Convert PDF to PPTX ----------
            // PptxSaveOptions is used for PDF‑to‑PPTX conversion.
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Cache glyphs can improve performance for large documents.
                CacheGlyphs = true
            };

            // Save the PDF as a PPTX presentation.
            pdfDoc.Save(pptxPath, pptxOptions);
            Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
        }
    }
}