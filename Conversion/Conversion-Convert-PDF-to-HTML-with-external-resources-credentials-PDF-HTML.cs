using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output HTML file path
        const string htmlPath = "output.html";

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
                // Generate only the body content; header/footer are omitted
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                // Save raster images as external PNG files referenced via SVG
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                // Do not split the result into separate pages (single HTML file)
                SplitIntoPages = false,
                // Optional: set a title for the generated HTML page
                Title = "Converted HTML"
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(htmlPath, htmlOptions);
            Console.WriteLine($"PDF successfully converted to HTML at '{htmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}