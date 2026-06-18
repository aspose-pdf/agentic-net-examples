using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF and optimize it for web delivery
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                pdfDoc.Optimize(); // linearize for faster first‑page load

                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed raster images as Base64 inside SVG to keep a single HTML file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Produce one HTML file (not split per page)
                    SplitIntoPages = false,
                    // Remove empty top/bottom margins to shrink size
                    RemoveEmptyAreasOnTopAndBottom = true
                };

                // Convert PDF to HTML using the explicit SaveOptions
                pdfDoc.Save(outputHtmlPath, htmlOpts);
            }

            // ----- Minify the generated HTML -----
            string htmlContent = File.ReadAllText(outputHtmlPath);

            // Simple minification: remove comments and collapse whitespace between tags
            string minified = MinifyHtml(htmlContent);

            // Overwrite the original file with the minified version
            File.WriteAllText(outputHtmlPath, minified);

            Console.WriteLine($"PDF successfully converted and minified: {outputHtmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Very basic HTML minifier
    static string MinifyHtml(string html)
    {
        // Remove HTML comments
        string result = Regex.Replace(html, @"<!--(.*?)-->", string.Empty, RegexOptions.Singleline);
        // Collapse whitespace between tags (e.g., >   <  => ><
        result = Regex.Replace(result, @">\s+<", "><");
        // Trim leading/trailing whitespace
        return result.Trim();
    }
}