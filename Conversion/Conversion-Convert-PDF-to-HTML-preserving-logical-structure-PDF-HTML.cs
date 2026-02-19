using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input PDF and output HTML
        string inputPdfPath = "input.pdf";
        string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document (creation step)
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // NOTE: PreserveStructure property is not available in the current Aspose.Pdf version.
                // The logical structure is preserved by default when possible, so the line is omitted.

                // Generate a single HTML file (set to true to split per page)
                SplitIntoPages = false,

                // Use flow layout rather than fixed layout
                FixedLayout = false,

                // Embed raster images as PNG inside SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                // Generate full HTML markup
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // Save the document as HTML (save step)
            pdfDocument.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
