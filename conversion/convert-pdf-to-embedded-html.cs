using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Input PDF path – constant because it does not depend on runtime logic
    private const string InputPdf = "input.pdf";

    // Output HTML path – built at runtime, therefore static readonly
    private static readonly string OutputHtml = Path.Combine("HtmlResult", "result.html");

    static void Main()
    {
        // Verify input file exists
        if (!File.Exists(InputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {InputPdf}");
            return;
        }

        // Ensure the output directory exists (Path.GetDirectoryName can return null, guard against it)
        string outputDir = Path.GetDirectoryName(OutputHtml) ?? string.Empty;
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(InputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (fonts, images, CSS) into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded into SVG for better compatibility
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ (Windows only); handle possible platform exceptions
                try
                {
                    pdfDoc.Save(OutputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved to '{OutputHtml}'.");
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
