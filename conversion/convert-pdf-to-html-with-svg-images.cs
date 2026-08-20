using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Desired HTML output file
        const string htmlPath = "output.html";

        // Folder where generated SVG images will be stored
        const string svgFolder = "svg_images";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the SVG folder exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Store SVG images in the specified folder
                    SpecialFolderForSvgImages = svgFolder,

                    // Example: save raster images as external PNG files referenced via SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // Save as HTML using the options (required to generate SVG images)
                pdfDoc.Save(htmlPath, htmlOpts);
            }

            Console.WriteLine($"Conversion completed. HTML saved to '{htmlPath}'. SVG images saved to '{svgFolder}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}