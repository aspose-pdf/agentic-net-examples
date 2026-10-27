using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath          = "input.pdf";
        const string htmlPath         = "output.html";
        const string minifiedHtmlPath = "output.min.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Convert PDF to HTML (Windows only – GDI+ required)
        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // HtmlSaveOptions must be passed explicitly; otherwise a PDF is written.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into a single HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Render images as PNGs embedded in SVG (cross‑platform friendly).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Produce a single HTML file (set to false if you want one file per page).
                    SplitIntoPages = false
                };

                doc.Save(htmlPath, htmlOpts);
            }

            Console.WriteLine($"PDF converted to HTML: {htmlPath}");
        }
        catch (TypeInitializationException)
        {
            // GDI+ not available on non‑Windows platforms.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // Minify the generated HTML file.
        try
        {
            string htmlContent = File.ReadAllText(htmlPath);

            // Simple minification: remove line breaks, tabs, and collapse multiple spaces.
            // This is a lightweight approach; for more aggressive minification use a dedicated library.
            string minified = Regex.Replace(htmlContent, @"\s+", " "); // collapse whitespace
            minified = minified.Replace("> <", "><");               // remove spaces between tags

            File.WriteAllText(minifiedHtmlPath, minified.Trim());

            Console.WriteLine($"Minified HTML saved to: {minifiedHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML minification: {ex.Message}");
        }
    }
}