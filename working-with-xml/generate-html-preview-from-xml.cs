using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input_from_xml.pdf";   // PDF generated from XML
        const string outputHtmlPath = "preview.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources (fonts, images) into the HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Render images as PNGs embedded into SVG (cross‑browser friendly)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                // Optional: split each PDF page into a separate HTML file
                // SplitIntoPages = true
            };

            // HTML conversion relies on GDI+ and works only on Windows.
            // Wrap the save call to handle non‑Windows platforms gracefully.
            try
            {
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"HTML preview saved to '{outputHtmlPath}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires GDI+ (Windows only). Operation skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML conversion: {ex.Message}");
            }
        }
    }
}