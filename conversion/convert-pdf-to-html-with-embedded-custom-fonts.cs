using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontSource classes are in Aspose.Pdf.Text namespace

class PdfToHtmlWithCustomFonts
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output HTML file path
        const string outputHtml = "output.html";

        // Path to a custom TrueType font that should be embedded in the HTML
        const string customFontPath = "custom-font.ttf";

        // Verify that the required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options (must be passed explicitly)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Embed all referenced fonts as WOFF files (ensures @font-face rules are generated)
                htmlOpts.FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF;

                // Add the custom font to the FontSources collection.
                // This makes the converter include the font in the generated CSS via @font-face.
                htmlOpts.FontSources.Add(new FileFontSource(customFontPath));

                // Optional: set the default font name to the custom font.
                // If the PDF uses a font that is not embedded, this name will be used as a fallback.
                // htmlOpts.DefaultFontName = Path.GetFileNameWithoutExtension(customFontPath);

                // Save the PDF as HTML using the configured options.
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML with embedded custom fonts: {outputHtml}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
