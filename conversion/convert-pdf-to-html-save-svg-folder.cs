using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string svgFolder = "SvgImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the folder that will hold extracted SVG images
        Directory.CreateDirectory(svgFolder);

        // HTML conversion (requires GDI+ on Windows)
        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Store each generated SVG image in the dedicated folder
                    SpecialFolderForSvgImages = svgFolder,

                    // Optional: embed raster images as PNG inside the generated SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Optional: embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Save the PDF as HTML using the configured options
                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML. SVG images saved in '{svgFolder}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // GDI+ is unavailable on non‑Windows platforms
            Console.WriteLine("HTML conversion requires GDI+ and is only supported on Windows.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}