using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and desired HTML output paths
        const string pdfPath   = "input.pdf";
        const string htmlPath  = "output.html";

        // Custom font files to embed (ensure these files exist next to the executable or provide full paths)
        var customFonts = new List<string>
        {
            "fonts/MyFont-Regular.ttf",
            "fonts/MyFont-Bold.ttf"
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Convert PDF to HTML with explicit HtmlSaveOptions (required for non‑PDF output)
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Embed all resources (images, CSS) into the HTML folder
            PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
            // Render images as PNG inside SVG (cross‑platform safe)
            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
            // Do not split into separate pages (single HTML file)
            SplitIntoPages = false
        };

        try
        {
            // Load PDF and save as HTML inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                pdfDoc.Save(htmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion on this platform.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // Determine the generated CSS file path (Aspose.Pdf creates a .css file with the same name as the HTML)
        string cssPath = Path.ChangeExtension(htmlPath, ".css");
        if (!File.Exists(cssPath))
        {
            Console.Error.WriteLine($"CSS file not found: {cssPath}");
            return;
        }

        // Read existing CSS content
        string cssContent = File.ReadAllText(cssPath);

        // Build @font-face rules for each custom font
        var fontFaceRules = new List<string>();
        foreach (string fontFile in customFonts)
        {
            if (!File.Exists(fontFile))
            {
                Console.Error.WriteLine($"Font file not found: {fontFile}");
                continue; // skip missing fonts
            }

            // Derive a font family name from the file name (without extension)
            string fontFamily = Path.GetFileNameWithoutExtension(fontFile).Replace(' ', '_');

            // Build relative URL from the CSS file location to the font file
            string relativePath = GetRelativePath(cssPath, fontFile);

            string rule = $@"
@font-face {{
    font-family: '{fontFamily}';
    src: url('{relativePath}') format('truetype');
    font-weight: normal;
    font-style: normal;
}}";
            fontFaceRules.Add(rule);
        }

        // Prepend the @font-face rules to the existing CSS
        string newCssContent = string.Join(Environment.NewLine, fontFaceRules) + Environment.NewLine + cssContent;
        File.WriteAllText(cssPath, newCssContent);

        Console.WriteLine($"Custom fonts embedded via @font-face rules in CSS: {cssPath}");
    }

    // Helper to compute a relative path from one file to another
    static string GetRelativePath(string fromPath, string toPath)
    {
        Uri fromUri = new Uri(Path.GetDirectoryName(Path.GetFullPath(fromPath)) + Path.DirectorySeparatorChar);
        Uri toUri   = new Uri(Path.GetFullPath(toPath));
        Uri relUri  = fromUri.MakeRelativeUri(toUri);
        return Uri.UnescapeDataString(relUri.ToString().Replace('/', Path.DirectorySeparatorChar));
    }
}