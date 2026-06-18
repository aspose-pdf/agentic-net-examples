using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;               // For HtmlSaveOptions enums

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";
        const string htmlPath     = "output.html";
        const string customCssPath = "custom.css";   // Your CSS file

        // Verify source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(customCssPath))
        {
            Console.Error.WriteLine($"Custom CSS not found: {customCssPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options (creation rule)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Example: embed raster images into SVG to keep a single HTML file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: generate a single HTML page (default)
                    SplitIntoPages = false
                };

                // Perform conversion – must pass HtmlSaveOptions for non‑PDF output
                try
                {
                    pdfDoc.Save(htmlPath, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ (Windows only)
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
                    return;
                }
            }

            // At this point the HTML file exists. Insert a reference to the custom CSS.
            string htmlContent = File.ReadAllText(htmlPath);

            // Find the <head> tag (case‑insensitive) and inject the <link> after it.
            // Simple approach – replace first occurrence of "<head>".
            const string headTag = "<head>";
            int headIndex = htmlContent.IndexOf(headTag, StringComparison.OrdinalIgnoreCase);
            if (headIndex >= 0)
            {
                int insertPos = headIndex + headTag.Length;
                string linkTag = $"\n<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(customCssPath)}\" />\n";
                htmlContent = htmlContent.Insert(insertPos, linkTag);
                File.WriteAllText(htmlPath, htmlContent);
                Console.WriteLine($"HTML saved to '{htmlPath}' with custom CSS linked.");
            }
            else
            {
                // Fallback: prepend the link at the beginning of the file
                string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(customCssPath)}\" />\n";
                File.WriteAllText(htmlPath, linkTag + htmlContent);
                Console.WriteLine($"HTML saved to '{htmlPath}'. <head> tag not found; CSS link added at file start.");
            }

            // Optionally copy the custom CSS file to the same directory as the HTML output
            string destCssPath = Path.Combine(Path.GetDirectoryName(htmlPath) ?? "", Path.GetFileName(customCssPath));
            File.Copy(customCssPath, destCssPath, overwrite: true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}