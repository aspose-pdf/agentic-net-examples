using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, HtmlSaveOptions, etc.)

class PdfToSinglePageHtml
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";

        // Output HTML file path (single HTML file containing all pages)
        const string outputHtmlPath = "output.html";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize HTML save options.
                // No special options are required for a single‑page HTML output;
                // by default Aspose.Pdf creates one HTML file that contains all pages.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Optional: preserve fonts as Web Open Font Format (WOFF) to ensure they are embedded.
                // htmlOptions.FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF;

                // Save the PDF as a single HTML file.
                pdfDoc.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{outputHtmlPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}