using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string pdfPath = "input.pdf";

        // Output HTML path (single file)
        const string htmlPath = "output.html";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Convert PDF to HTML using HtmlSaveOptions (required for non‑PDF output)
        using (Document pdfDoc = new Document(pdfPath))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Optional: embed all resources into the HTML to keep a single file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Optional: reduce image size by embedding as PNG inside SVG
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            pdfDoc.Save(htmlPath, htmlOpts);
        }

        // Simple HTML minification: remove comments, line breaks and excess whitespace
        try
        {
            string htmlContent = File.ReadAllText(htmlPath);

            // Remove HTML comments
            htmlContent = Regex.Replace(htmlContent, @"<!--(.*?)-->", string.Empty, RegexOptions.Singleline);

            // Collapse whitespace between tags
            htmlContent = Regex.Replace(htmlContent, @">\s+<", "><");

            // Trim leading/trailing whitespace
            htmlContent = htmlContent.Trim();

            // Overwrite the original file with minified content
            File.WriteAllText(htmlPath, htmlContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML minification: {ex.Message}");
        }

        Console.WriteLine($"PDF successfully converted and minified to HTML: {htmlPath}");
    }
}