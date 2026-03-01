using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Open the PDF from a file stream (lifecycle: create stream, load document, dispose both)
            using (FileStream pdfStream = File.OpenRead(inputPdfPath))
            using (Document doc = new Document(pdfStream))
            {
                // Prepare HTML save options (required for non‑PDF output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all resources into the HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded into SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML. On non‑Windows platforms this may throw TypeInitializationException
                try
                {
                    doc.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"HTML saved to '{outputHtmlPath}'.");
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