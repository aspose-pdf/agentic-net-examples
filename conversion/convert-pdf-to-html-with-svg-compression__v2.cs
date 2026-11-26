using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.html";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // HtmlSaveOptions must be supplied; otherwise a PDF is written regardless of extension
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Enable compression of any SVG graphics embedded in the HTML output
                    CompressSvgGraphicsIfAny = true,

                    // Optional: embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Optional: rasterize images as PNGs embedded in SVG (common for Windows GDI+ conversion)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML using the configured options
                doc.Save(outputPath, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputPath}");
            }
        }
        // HTML conversion via Aspose.Pdf requires GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}