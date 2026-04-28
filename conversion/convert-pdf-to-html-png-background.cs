using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF document (using block ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Render page backgrounds as PNG images by using the appropriate raster image saving mode
                htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;

                // (Optional) Render other raster images as embedded PNG page backgrounds
                // htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;

                // Save as HTML using explicit options (required for non‑PDF output)
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
