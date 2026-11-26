using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string svgFolder = "SvgFolder";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolder);

        // HTML conversion (Windows only) – wrap in try‑catch for GDI+ limitation
        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure HTML save options, including the folder for generated SVG images
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Store SVG images in the specified folder
                    SpecialFolderForSvgImages = svgFolder,
                    // Optional: embed raster images as PNGs inside the generated SVGs
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the explicit HtmlSaveOptions (required to get HTML output)
                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF converted to HTML. SVG images saved in '{svgFolder}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // GDI+ not available on non‑Windows platforms
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}