using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string cssPath = "responsive.css";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure a responsive CSS file exists; create a simple one if missing
        if (!File.Exists(cssPath))
        {
            File.WriteAllText(cssPath,
@"/* Simple responsive CSS */
body { margin:0; padding:0; font-family:Arial,Helvetica,sans-serif; }
img { max-width:100%; height:auto; }
@media only screen and (max-width:600px) {
    body { font-size:14px; }
    .page { padding:5px; }
}");
        }

        try
        {
            // Load PDF and convert to HTML with explicit HtmlSaveOptions (required on all platforms)
            using (Document pdfDoc = new Document(inputPdf))
            {
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed raster images as PNG inside SVG to preserve layout
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Keep output as a single HTML file
                    SplitIntoPages = false
                };

                pdfDoc.Save(outputHtml, htmlOptions);
            }

            // Post‑process the generated HTML to reference the responsive stylesheet
            string htmlContent = File.ReadAllText(outputHtml);
            string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssPath)}\" />";

            if (htmlContent.Contains("<head>"))
            {
                htmlContent = htmlContent.Replace("<head>", $"<head>{Environment.NewLine}{linkTag}");
            }
            else
            {
                // Fallback: prepend the link if <head> is missing
                htmlContent = linkTag + Environment.NewLine + htmlContent;
            }

            File.WriteAllText(outputHtml, htmlContent);

            Console.WriteLine($"PDF successfully converted to HTML with responsive CSS: {outputHtml}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}