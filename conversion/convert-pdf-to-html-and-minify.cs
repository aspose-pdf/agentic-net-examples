using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output HTML files
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using the recommended using block for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed raster images as PNG inside SVG to keep a single HTML file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: generate only the body content if you need a fragment
                    // HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent
                };

                // Convert PDF to HTML. Wrap in try‑catch because HTML conversion requires GDI+ (Windows only)
                try
                {
                    pdfDocument.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.Error.WriteLine("HTML conversion requires Windows GDI+. Skipping conversion on this platform.");
                    return;
                }
            }

            // ----- Minify the generated HTML -----
            // Read the HTML content
            string htmlContent = File.ReadAllText(outputHtmlPath);

            // Remove HTML comments
            htmlContent = Regex.Replace(htmlContent, @"<!--(.*?)-->", string.Empty, RegexOptions.Singleline);

            // Collapse whitespace between tags and within text nodes
            // 1. Replace sequences of whitespace characters (space, tab, newline) with a single space
            htmlContent = Regex.Replace(htmlContent, @"\s+", " ");

            // 2. Remove spaces between tags: > <  => ><
            htmlContent = Regex.Replace(htmlContent, @">\s+<", "><");

            // Trim leading/trailing whitespace
            htmlContent = htmlContent.Trim();

            // Write the minified HTML back to the same file (or to a new file if preferred)
            File.WriteAllText(outputHtmlPath, htmlContent);
            Console.WriteLine($"HTML minification completed: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}