using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Render all background images as a single PNG per page
                // This uses the AsEmbeddedPartsOfPngPageBackground mode (value 2)
                htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;

                // Optional: embed all resources (CSS, images, fonts) into the HTML file
                // htmlOpts.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Save the PDF as HTML using the configured options
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML with PNG page backgrounds: {outputHtml}");
        }
        // HTML conversion relies on GDI+ and is Windows‑only; handle the platform limitation gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}