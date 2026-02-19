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
            Console.Error.WriteLine("Usage: PdfToHtmlConverter <input-pdf> <output-html>");
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

            // Configure custom HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Generate only the body content (no <html>, <head>, <body> wrappers)
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,

                // Do not split the PDF into separate HTML files per page
                SplitIntoPages = false,

                // Use fixed layout if you need a pixel‑perfect representation; otherwise keep flow layout
                FixedLayout = false,

                // Store raster images as PNGs embedded into SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                // Optional: set a title for the generated HTML page
                Title = Path.GetFileNameWithoutExtension(pdfPath)
            };

            // Save the PDF as HTML using the custom options
            pdfDocument.Save(htmlPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}