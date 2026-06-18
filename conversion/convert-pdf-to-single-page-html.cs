using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output HTML file path (single HTML file containing all pages)
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all resources (images, CSS, fonts) into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Preserve original fonts by embedding them (WOFF format by default)
                    FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF,

                    // Ensure the result is a single HTML page (default behavior)
                    SplitIntoPages = false
                };

                // Save the PDF as a single-page HTML file
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{outputHtmlPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}