using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes and HtmlSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Desired output HTML file path
        const string htmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Generate only the content inside the <body> tag
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                // Embed raster images as PNG data inside SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                // Produce a single HTML file (no per‑page splitting)
                SplitIntoPages = false
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