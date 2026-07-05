using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path (single-page HTML)
        const string htmlPath = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize HTML save options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Preserve fonts in all formats (WOFF, TTF, EOT) for maximum compatibility
                    FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats,

                    // Ensure the result is a single HTML file
                    SplitIntoPages = false
                };

                // Save the PDF as a single-page HTML preserving layout and fonts
                pdfDocument.Save(htmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{htmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
