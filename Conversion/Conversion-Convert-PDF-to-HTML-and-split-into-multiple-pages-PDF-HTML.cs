using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output folder where HTML pages will be saved
        const string outputFolder = "output_html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Split each PDF page into a separate HTML file
                SplitIntoPages = true,

                // Embed raster images as PNG inside SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                // Write full HTML markup for each page
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // Base file name for generated HTML pages.
            // Aspose.Pdf will append page numbers (e.g., page1.html, page2.html, …)
            string baseHtmlPath = Path.Combine(outputFolder, "page.html");

            // Save the PDF as HTML with the configured options
            pdfDocument.Save(baseHtmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
