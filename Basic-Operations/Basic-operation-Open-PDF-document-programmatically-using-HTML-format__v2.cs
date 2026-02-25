using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Desired HTML output file path
        const string outputHtml = "output.html";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPdf))
            {
                // Example operation: display page count
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Example operation: extract all text using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Text characters: {absorber.Text.Length}");

                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (CSS, images, fonts) directly into the HTML
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNGs embedded in SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML. Wrap in try-catch because HTML conversion requires GDI+ (Windows only)
                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML → {outputHtml}");
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