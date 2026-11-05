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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options. The CompressSvgImages property is not
                // available in the referenced Aspose.Pdf version, so it is omitted. The SDK
                // will use its default SVG compression behavior.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // HTML conversion relies on GDI+ (Windows only). Wrap in try‑catch for cross‑platform safety.
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
                catch (DllNotFoundException)
                {
                    Console.WriteLine("GDI+ library not found. HTML conversion is unavailable on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
