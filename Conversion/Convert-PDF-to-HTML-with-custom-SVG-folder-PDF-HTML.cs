using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string svgFolderPath = "SvgImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for SVG images exists.
        Directory.CreateDirectory(svgFolderPath);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Store generated SVG images in the custom folder.
                    SpecialFolderForSvgImages = svgFolderPath,
                    // Example: store raster images as external PNG files referenced via SVG.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // HTML conversion relies on GDI+ (Windows only). Handle possible platform exception.
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtmlPath}'. SVG images saved in '{svgFolderPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}