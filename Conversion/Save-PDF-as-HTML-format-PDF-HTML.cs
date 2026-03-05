using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load).
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize HTML save options (required for non‑PDF output).
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Example: embed all resources into a single HTML file.
                htmlOpts.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Example: embed raster images as PNG inside SVG wrappers.
                htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

                // HTML conversion uses GDI+ and may fail on non‑Windows platforms.
                // Wrap the save call to handle that scenario gracefully.
                try
                {
                    // Save the document as HTML (lifecycle: save).
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML file saved to '{outputHtml}'.");
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