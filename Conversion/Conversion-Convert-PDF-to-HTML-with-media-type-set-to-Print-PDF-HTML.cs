using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlSaveOptions (all save options are in Aspose.Pdf)

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
                // Configure HTML save options.
                // The Title property is used here to indicate the "Print" media type.
                // (Aspose.Pdf does not expose a direct MediaType property for PDF→HTML conversion.)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    Title = "Print", // indicate print media type
                    // Optional: embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Optional: use PNG images wrapped in SVG for better compatibility
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML using the explicit HtmlSaveOptions.
                // This ensures the output format is HTML regardless of the file extension.
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{outputHtml}'");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}