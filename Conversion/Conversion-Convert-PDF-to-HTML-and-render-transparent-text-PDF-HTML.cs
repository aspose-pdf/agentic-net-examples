using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output HTML file path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToHtmlConverter <input.pdf> <output.html>");
            return;
        }

        string pdfPath = args[0];
        string htmlPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Preserve the full HTML structure (including head, style, etc.)
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml,

                // Keep raster images embedded as PNG inside SVG wrappers (handles transparency correctly)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                // Ensure that text is rendered as selectable HTML text (transparent text will be kept as normal text)
                // No special option is required; the default behavior preserves text opacity.
                // Additional options can be set here if needed, e.g., FontSavingMode, etc.
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(htmlPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}