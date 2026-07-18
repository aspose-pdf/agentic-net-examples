using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class PdfToHtmlWithToc
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Convert PDF to HTML
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed images as PNG inside SVG to keep a single HTML file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Generate only the body content (optional, can be changed)
                    HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
                };

                pdfDoc.Save(htmlPath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion.");
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
            htmlContent = File.ReadAllText(htmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read generated HTML: {ex.Message}");
            return;
        }

        // Find headings (h1‑h6) and ensure each has an id attribute
        Regex headingPattern = new Regex(@"<(h[1-6])([^>]*)>(.*?)</\1>", RegexOptions.IgnoreCase);
        var matches = headingPattern.Matches(htmlContent);
        StringBuilder tocBuilder = new StringBuilder();
        tocBuilder.AppendLine("<nav class=\"toc\"><ul>");

        int headingIndex = 0;
        foreach (Match match in matches)
        {
            string tagName = match.Groups[1].Value.ToLower(); // h1 … h6
            string attributes = match.Groups[2].Value;
            string innerHtml = match.Groups[3].Value;

            // Generate a unique id if missing
            string id;
            var idMatch = Regex.Match(attributes, @"\sid\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
            if (idMatch.Success)
            {
                id = idMatch.Groups[1].Value;
            }
            else
            {
                id = $"heading_{headingIndex++}";
                // Insert id attribute into the heading tag
                string newTag = $"<{tagName} id=\"{id}\"{attributes}>{innerHtml}</{tagName}>";
                htmlContent = htmlContent.Replace(match.Value, newTag);
            }

            // Build TOC entry with indentation based on heading level
            int level = int.Parse(tagName.Substring(1));
            tocBuilder.AppendLine($"{new string(' ', (level - 1) * 2)}<li><a href=\"#{id}\">{StripHtml(innerHtml)}</a></li>");
        }

        tocBuilder.AppendLine("</ul></nav>");

        // Insert TOC after opening <body> tag
        Regex bodyOpenPattern = new Regex(@"<body[^>]*>", RegexOptions.IgnoreCase);
        var bodyMatch = bodyOpenPattern.Match(htmlContent);
        if (bodyMatch.Success)
        {
            int insertPos = bodyMatch.Index + bodyMatch.Length;
            htmlContent = htmlContent.Insert(insertPos, Environment.NewLine + tocBuilder.ToString() + Environment.NewLine);
        }
        else
        {
            // Fallback: prepend TOC at the beginning
            htmlContent = tocBuilder.ToString() + Environment.NewLine + htmlContent;
        }

        // Write the modified HTML back to file
        try
        {
            File.WriteAllText(htmlPath, htmlContent);
            Console.WriteLine($"HTML with TOC saved to '{htmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write modified HTML: {ex.Message}");
        }
    }

    // Helper to remove any nested HTML tags from heading text
    private static string StripHtml(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty);
    }
}