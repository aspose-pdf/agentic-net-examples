using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main(string[] args)
    {
        // Input HTML file path (first argument or default)
        string htmlPath = args.Length > 0 ? args[0] : "input.html";
        // Output PDF file path (second argument or default)
        string pdfPath = args.Length > 1 ? args[1] : "output.pdf";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – '{htmlPath}'.");
            return;
        }

        try
        {
            // Create load options for HTML → PDF conversion
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                // Example of a custom option: render the whole document on a single page
                IsRenderToSinglePage = false
            };

            // Load the HTML document into an Aspose.Pdf Document object
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // (Optional) Demonstrate creation of HtmlSaveOptions – not used for saving PDF,
            // but shown here as a custom object per the task description.
            HtmlSaveOptions htmlSaveOptions = new HtmlSaveOptions
            {
                // Generate only the body content (no <html>/<head> wrapper)
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                // Split each PDF page into a separate HTML file (if we were saving to HTML)
                SplitIntoPages = false,
                // Use PNG images embedded into SVG for raster graphics
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            // The htmlSaveOptions object can be used when saving a PDF to HTML:
            // pdfDocument.Save("output.html", htmlSaveOptions);
            // In this example we are converting HTML → PDF, so we save directly to PDF.

            // Save the resulting PDF file
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"HTML file '{htmlPath}' successfully converted to PDF '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}