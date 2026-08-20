using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for FontSource classes

class PdfToHtmlWithCustomFonts
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path (the main HTML file)
        const string htmlPath = "output.html";

        // Path to the custom font file you want to embed (e.g., a .ttf file)
        const string customFontPath = "custom-font.ttf";

        // Verify that the input files exist
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
                // Initialize HTML save options (lifecycle: create)
                HtmlSaveOptions saveOptions = new HtmlSaveOptions();

                // ------------------------------------------------------------
                // Font embedding configuration
                // ------------------------------------------------------------
                // Save all referenced fonts as WOFF (Web Open Font Format)
                // This ensures the generated CSS contains @font-face rules.
                saveOptions.FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF;

                // Add the custom font to the FontSources collection.
                // The converter will embed this font and generate the appropriate @font-face rule.
                saveOptions.FontSources.Add(new FileFontSource(customFontPath));

                // Optionally, specify a default font name to be used when a PDF font is missing.
                // The name should match the internal name of the custom font (without extension).
                // saveOptions.DefaultFontName = "CustomFontName";

                // ------------------------------------------------------------
                // Save the PDF as HTML using the configured options (lifecycle: save)
                // ------------------------------------------------------------
                pdfDocument.Save(htmlPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML with embedded custom fonts: {htmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}