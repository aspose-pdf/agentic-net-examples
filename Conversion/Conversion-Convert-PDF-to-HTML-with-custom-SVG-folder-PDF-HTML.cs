using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path (main HTML file)
        const string htmlPath = "output.html";

        // Folder where generated SVG files and external PNG images will be stored
        const string svgFolder = "svg_output";

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

            // Ensure the custom SVG folder exists (Aspose will place SVG files there when the property is supported)
            if (!Directory.Exists(svgFolder))
                Directory.CreateDirectory(svgFolder);

            // Configure HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Save each raster image as an external PNG file and reference it via a wrapping SVG
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                // NOTE: The property 'CustomSvgFolderPath' is not available in older Aspose.Pdf versions.
                // If you are using a version that supports it, you can set it here:
                // CustomSvgFolderPath = svgFolder
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(htmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
