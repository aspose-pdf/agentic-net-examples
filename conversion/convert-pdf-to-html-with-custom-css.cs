using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";   // source PDF
        const string htmlPath = "output.html"; // generated HTML
        const string cssPath  = "custom.css"; // custom CSS to apply

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(cssPath))
        {
            Console.Error.WriteLine($"CSS not found: {cssPath}");
            return;
        }

        try
        {
            // Load PDF and convert to HTML using HtmlSaveOptions
            using (Document pdfDoc = new Document(pdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();
                // Example setting – embed raster images as PNG inside SVG wrappers
                htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;
                pdfDoc.Save(htmlPath, htmlOpts);
            }

            // Read the custom CSS content
            string customCss = File.ReadAllText(cssPath);

            // Load the generated HTML
            string htmlContent = File.ReadAllText(htmlPath);

            // Insert the CSS into the <head> section (if present)
            const string headTag = "<head>";
            int headPos = htmlContent.IndexOf(headTag, StringComparison.OrdinalIgnoreCase);
            if (headPos >= 0)
            {
                int insertPos = headPos + headTag.Length;
                string styleBlock = $"\n<style>\n{customCss}\n</style>\n";
                htmlContent = htmlContent.Insert(insertPos, styleBlock);
            }
            else
            {
                // Fallback: prepend a <style> block at the beginning of the file
                string styleBlock = $"<style>\n{customCss}\n</style>\n";
                htmlContent = styleBlock + htmlContent;
            }

            // Save the modified HTML back to disk
            File.WriteAllText(htmlPath, htmlContent);
            Console.WriteLine($"Conversion completed. HTML saved to '{htmlPath}' with custom CSS applied.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}