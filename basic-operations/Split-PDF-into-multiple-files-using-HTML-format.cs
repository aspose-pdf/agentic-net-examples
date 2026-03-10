using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html"; // Base name; split pages will be saved as output.html, output_1.html, ...

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options to split each PDF page into its own HTML file.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed all resources (fonts, images, CSS) into the generated HTML files.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded into SVG (cross‑platform friendly).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML using the explicit HtmlSaveOptions.
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine("PDF successfully split into separate HTML pages.");
        }
        // HTML conversion relies on GDI+, which is Windows‑only.
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}