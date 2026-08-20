using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path
        const string htmlPath = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Generate a single HTML file (default)
                    SplitIntoPages = false,

                    // Embed images as PNG inside SVG to keep a single file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Optional: remove empty top/bottom margins
                    RemoveEmptyAreasOnTopAndBottom = true
                };

                // Convert PDF to HTML
                pdfDocument.Save(htmlPath, htmlOptions);
            }

            // Minify the generated HTML
            string htmlContent = File.ReadAllText(htmlPath);

            // Remove HTML comments
            htmlContent = Regex.Replace(htmlContent, @"<!--(.*?)-->", string.Empty, RegexOptions.Singleline);

            // Collapse multiple whitespace characters into a single space
            htmlContent = Regex.Replace(htmlContent, @"\s+", " ");

            // Remove spaces between tags
            htmlContent = Regex.Replace(htmlContent, @">\s+<", "><");

            // Trim leading/trailing whitespace
            htmlContent = htmlContent.Trim();

            // Overwrite the HTML file with the minified content
            File.WriteAllText(htmlPath, htmlContent);

            Console.WriteLine($"PDF successfully converted and minified: {htmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is Windows‑only
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}