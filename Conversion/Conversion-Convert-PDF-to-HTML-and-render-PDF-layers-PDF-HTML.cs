using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main(string[] args)
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Generate a single HTML file (set to true to get one file per page)
                SplitIntoPages = false,

                // NOTE: The RenderLayers property is not available in the current Aspose.Pdf version.
                // It has been removed. If you need layer rendering, use an older Aspose.Pdf version that supports it.

                // How raster images are saved in the HTML output
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                // Control the amount of HTML markup generated
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to HTML. Output saved at '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
