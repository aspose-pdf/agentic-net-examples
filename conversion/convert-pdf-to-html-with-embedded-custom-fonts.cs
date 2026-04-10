using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // Required for FontSource if needed

class PdfToHtmlWithCustomFonts
{
    static void Main()
    {
        // Input PDF and output HTML paths
        const string pdfPath  = "input.pdf";
        const string htmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Embed fonts in all three web‑compatible formats (WOFF, TTF, EOT)
            // This ensures that the generated CSS contains @font-face rules
            // referencing the saved font files.
            htmlOptions.FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats;

            // Optional: specify a fallback font name that will be used when a
            // PDF font is not embedded. Replace "MyCustomFont" with the actual
            // name of the font you want to appear in the CSS.
            htmlOptions.DefaultFontName = "MyCustomFont";

            // Optional: if you have additional font files that you want to make
            // available to the converter (e.g., fonts not embedded in the PDF),
            // add them to the FontSources collection. Each FontSource can be
            // created from a byte array representing the font file.
            // Example (uncomment and adjust the path if needed):
            // string customFontPath = "fonts/MyCustomFont.ttf";
            // if (File.Exists(customFontPath))
            // {
            //     byte[] fontData = File.ReadAllBytes(customFontPath);
            //     // FontSource constructor: (byte[] data, string fontName)
            //     FontSource fontSource = new FontSource(fontData, "MyCustomFont");
            //     htmlOptions.FontSources.Add(fontSource);
            // }

            // Save the PDF as HTML using the configured options (lifecycle: save)
            pdfDocument.Save(htmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML with embedded fonts: {htmlPath}");
    }
}