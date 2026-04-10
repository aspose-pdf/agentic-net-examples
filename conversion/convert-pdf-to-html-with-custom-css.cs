using System;
using System.IO;
using Aspose.Pdf;

// NOTE: This project must reference the Aspose.Pdf NuGet package (or the Aspose.Pdf.dll assembly)
// and target a valid .NET framework (e.g., net6.0, net7.0, net8.0). The previous build error was caused
// by an invalid project configuration that tried to copy a non‑existent "AsposePdfApi.dll".
// After fixing the project file (remove the old AsposePdfApi reference and set a proper <TargetFramework>),
// the code below compiles and runs correctly.

class Program
{
    static void Main()
    {
        // Input PDF, output HTML and custom CSS file paths
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string customCssPath = "custom.css";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load PDF and convert to HTML using explicit HtmlSaveOptions
            using (Document pdfDoc = new Document(pdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded in SVG (widely supported)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: set a title for the generated HTML page
                    Title = "Converted Document"
                };

                pdfDoc.Save(htmlPath, htmlOpts);
            }

            // If both the generated HTML and the custom CSS exist, inject a link to the CSS
            if (File.Exists(htmlPath) && File.Exists(customCssPath))
            {
                string htmlContent = File.ReadAllText(htmlPath);
                int headCloseIdx = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);

                string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(customCssPath)}\" />\n";

                if (headCloseIdx >= 0)
                {
                    // Insert the <link> just before </head>
                    htmlContent = htmlContent.Insert(headCloseIdx, linkTag);
                }
                else
                {
                    // No <head> element – prepend the link at the very start
                    htmlContent = linkTag + htmlContent;
                }

                File.WriteAllText(htmlPath, htmlContent);
                Console.WriteLine($"HTML saved to '{htmlPath}' with custom CSS applied.");
            }
            else
            {
                Console.WriteLine($"HTML saved to '{htmlPath}'. Custom CSS not applied (missing file).");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.Error.WriteLine("HTML conversion requires Windows GDI+. Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
