using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class PdfToHtmlWithToc
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Convert PDF to HTML
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Use PNG images wrapped in SVG (default)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                pdfDoc.Save(outputHtmlPath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // Read generated HTML
        string htmlContent;
        try
        {
            htmlContent = File.ReadAllText(outputHtmlPath, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read generated HTML: {ex.Message}");
            return;
        }

        // Build Table of Contents by scanning heading tags (h1‑h6)
        StringBuilder tocBuilder = new StringBuilder();
        tocBuilder.AppendLine("<nav class=\"toc\"><h2>Table of Contents</h2><ul>");

        // Regex to find heading tags, capture level and inner text
        Regex headingRegex = new Regex(@"<(h[1-6])([^>]*)>(.*?)</\1>", RegexOptions.IgnoreCase);
        int headingIndex = 0;

        // Replace headings to ensure they have an id attribute for linking
        string updatedHtml = headingRegex.Replace(htmlContent, match =>
        {
            string tagName = match.Groups[1].Value;          // h1, h2, etc.
            string attributes = match.Groups[2].Value;       // existing attributes
            string innerText = match.Groups[3].Value;        // heading text

            // Create a unique id for the heading
            string headingId = $"heading_{headingIndex++}";
            // Preserve existing id if present
            if (attributes.IndexOf("id=", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Extract existing id value
                var idMatch = Regex.Match(attributes, @"id\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
                if (idMatch.Success)
                    headingId = idMatch.Groups[1].Value;
            }

            // Build TOC entry with indentation based on heading level
            int level = int.Parse(tagName.Substring(1));
            string indent = new string(' ', (level - 1) * 4);
            tocBuilder.AppendLine($"{indent}<li><a href=\"#{headingId}\">{innerText}</a></li>");

            // Ensure the heading tag contains the id attribute
            string newAttributes;
            if (attributes.IndexOf("id=", StringComparison.OrdinalIgnoreCase) >= 0)
                newAttributes = attributes; // id already present
            else
                newAttributes = $"{attributes} id=\"{headingId}\"";

            return $"<{tagName}{newAttributes}>{innerText}</{tagName}>";
        });

        tocBuilder.AppendLine("</ul></nav>");

        // Insert TOC after the opening <body> tag
        Regex bodyTagRegex = new Regex(@"<body([^>]*)>", RegexOptions.IgnoreCase);
        string finalHtml = bodyTagRegex.Replace(updatedHtml, m => $"{m.Value}\n{tocBuilder}");

        // Write the modified HTML back to file
        try
        {
            File.WriteAllText(outputHtmlPath, finalHtml, Encoding.UTF8);
            Console.WriteLine($"HTML with TOC saved to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write final HTML: {ex.Message}");
        }
    }
}