using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string cssPath = "custom.css";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(cssPath))
        {
            Console.Error.WriteLine($"CSS file not found: {cssPath}");
            // Continue without CSS – we will simply not inject a <link> tag.
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                var htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    SplitIntoPages = false
                };

                doc.Save(htmlPath, htmlOpts);
                Console.WriteLine($"PDF converted to HTML: {htmlPath}");

                // If a CSS file exists, inject a <link> element that references it.
                if (File.Exists(cssPath))
                {
                    string htmlContent = File.ReadAllText(htmlPath);
                    string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssPath)}\" />";

                    int headIdx = htmlContent.IndexOf("<head>", StringComparison.OrdinalIgnoreCase);
                    if (headIdx >= 0)
                    {
                        int insertPos = headIdx + "<head>".Length;
                        htmlContent = htmlContent.Insert(insertPos, Environment.NewLine + "    " + linkTag);
                        File.WriteAllText(htmlPath, htmlContent);
                        Console.WriteLine($"Custom CSS linked in HTML: {cssPath}");
                    }
                    else
                    {
                        // Fallback – prepend the link if <head> is missing.
                        File.WriteAllText(htmlPath, linkTag + Environment.NewLine + htmlContent);
                        Console.WriteLine("<head> tag not found; CSS link prepended at file start.");
                    }
                }
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
