using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string fontsSourceFolder = "CustomFonts"; // folder containing your .ttf files
        const string fontsTargetFolder = "fonts";       // folder that will be referenced from HTML

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the fonts target folder exists and copy custom fonts there
        Directory.CreateDirectory(fontsTargetFolder);
        foreach (string fontFile in Directory.GetFiles(fontsSourceFolder, "*.ttf"))
        {
            string destPath = Path.Combine(fontsTargetFolder, Path.GetFileName(fontFile));
            File.Copy(fontFile, destPath, overwrite: true);
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options – use PartsEmbeddingMode to embed fonts and images
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed images as PNG inside SVG to keep everything in the HTML file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Embed all resources (fonts, images, CSS) directly into the HTML
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Do not split into separate pages – single HTML file
                    SplitIntoPages = false
                };

                // Save PDF as HTML (resources are embedded)
                pdfDoc.Save(outputHtmlPath, htmlOptions);
            }

            // After conversion, inject @font-face rules into the generated HTML
            string htmlContent = File.ReadAllText(outputHtmlPath);

            // Build CSS with @font-face rules pointing to the copied font files
            string fontCss = @"
<style>
@font-face {
    font-family: 'MyCustomFont';
    src: url('fonts/MyFont-Regular.ttf') format('truetype');
    font-weight: normal;
    font-style: normal;
}
@font-face {
    font-family: 'MyCustomFont';
    src: url('fonts/MyFont-Bold.ttf') format('truetype');
    font-weight: bold;
    font-style: normal;
}
</style>
";

            // Insert the CSS right after the opening <head> tag
            int headIndex = htmlContent.IndexOf("<head>", StringComparison.OrdinalIgnoreCase);
            if (headIndex >= 0)
            {
                int insertPos = headIndex + "<head>".Length;
                htmlContent = htmlContent.Insert(insertPos, fontCss);
            }
            else
            {
                // Fallback: prepend CSS at the very beginning
                htmlContent = fontCss + htmlContent;
            }

            // Write the modified HTML back to disk
            File.WriteAllText(outputHtmlPath, htmlContent);

            Console.WriteLine($"PDF successfully converted to HTML with embedded custom fonts: {outputHtmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
