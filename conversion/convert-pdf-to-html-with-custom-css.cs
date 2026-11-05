using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for HtmlSaveOptions

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string cssPath = "custom.css";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(cssPath))
        {
            Console.Error.WriteLine($"CSS file not found: {cssPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion options – the CssFileName property does not exist in the
                // Aspose.Pdf version used, so we will inject the CSS reference after the HTML file is created.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed raster images as PNG inside SVG to keep HTML self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Do not split into separate files – a single HTML file is produced
                    SplitIntoPages = false,
                    // Do not embed fonts or other parts – keep the HTML lightweight
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
                };

                // Save as HTML using the explicit HtmlSaveOptions
                doc.Save(htmlPath, htmlOptions);
                Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
            }

            // After the HTML file is generated, inject a <link> tag that points to the custom CSS file.
            // This approach works for all Aspose.Pdf versions because it does not rely on a missing API.
            InjectCssLink(htmlPath, cssPath);
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    /// <summary>
    /// Inserts a <link rel=\"stylesheet\" href=\"...\"> tag into the <head> section of the generated HTML file.
    /// If a <head> element cannot be found, the link is prepended to the file.
    /// </summary>
    private static void InjectCssLink(string htmlFilePath, string cssFilePath)
    {
        string htmlContent = File.ReadAllText(htmlFilePath, Encoding.UTF8);
        string cssFileName = Path.GetFileName(cssFilePath);
        string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{cssFileName}\" />";

        // Look for the closing </head> tag (case‑insensitive)
        int headCloseIndex = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
        if (headCloseIndex >= 0)
        {
            // Insert the link just before </head>
            htmlContent = htmlContent.Insert(headCloseIndex, linkTag + Environment.NewLine);
        }
        else
        {
            // No <head> found – prepend the link at the very beginning of the file
            htmlContent = linkTag + Environment.NewLine + htmlContent;
        }

        File.WriteAllText(htmlFilePath, htmlContent, Encoding.UTF8);
    }
}
