using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for font substitution utilities

class PdfToHtmlWithCustomFonts
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path
        const string htmlPath = "output.html";

        // Path to the custom font file you want to embed (TrueType or OpenType)
        const string customFontPath = "customfont.ttf";

        // Verify that required files exist
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

        // Register the custom font so that Aspose.Pdf can use it when the source PDF
        // references a missing font. This substitution will cause the converter to
        // embed the font as a WOFF file and generate the appropriate @font-face rule
        // in the resulting CSS.
        string customFontName = Path.GetFileNameWithoutExtension(customFontPath);
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(customFontName, customFontName));

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Prepare HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Ensure fonts are saved as web‑compatible files (WOFF in this example)
                FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF,
                // Set the default font name that will be used when the PDF references a
                // font that is not embedded. This helps the converter map missing fonts
                // to the custom one we supplied above.
                DefaultFontName = customFontName
            };

            // Save the PDF as HTML using the configured options.
            pdfDocument.Save(htmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML with embedded custom fonts: {htmlPath}");
    }
}
