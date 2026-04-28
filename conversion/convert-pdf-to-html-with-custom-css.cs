using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output HTML and the custom CSS file
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string customCssPath = "custom.css";

        // Verify that required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(customCssPath))
        {
            Console.Error.WriteLine($"Custom CSS file not found: {customCssPath}");
            return;
        }

        // Convert PDF to HTML using HtmlSaveOptions
        using (Document pdfDoc = new Document(pdfPath))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources into the generated HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Rasterize images and store them as external PNG files referenced via SVG
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                // Define the resolution for rasterized images (dpi)
                ImageResolution = 150,
                // Produce a single HTML file instead of splitting into multiple pages
                SplitIntoPages = false
            };

            pdfDoc.Save(htmlPath, htmlOpts);
        }

        // Insert a reference to the custom CSS file into the generated HTML
        string htmlContent = File.ReadAllText(htmlPath);
        int headCloseIdx = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
        if (headCloseIdx != -1)
        {
            string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(customCssPath)}\" />\n";
            htmlContent = htmlContent.Insert(headCloseIdx, linkTag);
            File.WriteAllText(htmlPath, htmlContent);
        }

        // Copy the custom CSS file to the same directory as the HTML output
        string destCssPath = Path.Combine(Path.GetDirectoryName(htmlPath) ?? string.Empty,
                                          Path.GetFileName(customCssPath));
        File.Copy(customCssPath, destCssPath, overwrite: true);

        Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        Console.WriteLine($"Custom CSS applied and copied to: {destCssPath}");
    }
}
