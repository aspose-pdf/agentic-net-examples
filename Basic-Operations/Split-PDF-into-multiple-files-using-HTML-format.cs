using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file pattern (first page will be saved as "output_page_1.html", etc.)
        const string htmlOutputPattern = "output_page_{0}.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed all resources into each HTML page (no external files)
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Optional: embed raster images as PNG inside SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // The Save method will generate one HTML file per PDF page.
                // The file name pattern can include a placeholder {0} which will be replaced
                // by the page number (1‑based). Here we use string.Format to build the name.
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    string htmlFile = string.Format(htmlOutputPattern, i);
                    doc.Save(htmlFile, htmlOptions);
                    Console.WriteLine($"Page {i} → {htmlFile}");
                }
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ which is only available on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping operation on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}