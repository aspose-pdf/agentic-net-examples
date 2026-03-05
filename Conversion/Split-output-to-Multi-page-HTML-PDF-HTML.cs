using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "output_html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the folder for the generated HTML files exists.
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion to produce one HTML file per PDF page.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Example: embed raster images as PNG inside SVG.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Base file name for the generated pages. Aspose will create
                // page_1.html, page_2.html, ... in the same directory.
                string baseHtmlPath = Path.Combine(outputFolder, "page.html");

                // Save the PDF as multi‑page HTML.
                doc.Save(baseHtmlPath, htmlOpts);
            }

            Console.WriteLine("PDF successfully split into multi‑page HTML.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}