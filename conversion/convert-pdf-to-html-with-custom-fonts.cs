using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for FileFontSource

class PdfToHtmlWithCustomFonts
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path
        const string htmlPath = "output.html";

        // Path to the custom font file you want to embed (TrueType font)
        const string customFontPath = "custom-font.ttf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font file not found: {customFontPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Prepare HTML save options (lifecycle: create)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Ensure fonts are saved as web‑font files (WOFF) so that @font-face rules are generated
                htmlOptions.FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF;

                // Add the custom font to the FontSources collection.
                // The converter will embed this font and generate the appropriate @font-face rule.
                FileFontSource customFontSource = new FileFontSource(customFontPath);
                htmlOptions.FontSources.Add(customFontSource);

                // Optional: embed all resources (fonts, images, CSS) directly into the HTML file.
                // Uncomment the line below if you prefer a single self‑contained HTML file.
                // htmlOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Save the PDF as HTML using the configured options (lifecycle: save)
                pdfDocument.Save(htmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML with embedded custom fonts: {htmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}