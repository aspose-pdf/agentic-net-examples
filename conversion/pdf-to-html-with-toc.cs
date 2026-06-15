using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class PdfToHtmlWithToc
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string finalHtmlPath = "output_with_toc.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to HTML ----------
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                // HtmlSaveOptions must be passed explicitly; otherwise a PDF is written.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Generate a single HTML file (default).
                    SplitIntoPages = false,
                    // Embed images as PNG inside SVG to keep the HTML self‑contained.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Use the full markup generation mode.
                    HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
                };

                pdfDoc.Save(htmlPath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and works only on Windows.
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // ---------- Build Table of Contents ----------
        StringBuilder tocBuilder = new StringBuilder();
        tocBuilder.AppendLine("<nav>");
        tocBuilder.AppendLine("<h2>Table of Contents</h2>");
        tocBuilder.AppendLine("<ul>");

        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Access tagged content (requires the PDF to be tagged).
                ITaggedContent tagged = pdfDoc.TaggedContent;

                // Find all header elements (H1‑H6) in the structure tree.
                var headers = tagged.RootElement.FindElements<HeaderElement>(true);

                foreach (HeaderElement header in headers)
                {
                    // Retrieve the heading text.
                    string headingText = header.ActualText ?? string.Empty;

                    // HeaderElement in the current Aspose.Pdf version does not expose Level or PageNumber.
                    // Use default values: level = 1 (no indentation) and pageNumber = 1 (link to first page).
                    int level = 1;
                    int pageNumber = 1;

                    // Build an anchor link to the corresponding page in the generated HTML.
                    // Aspose HTML output creates an element with id="page{n}" for each page.
                    string anchor = $"#page{pageNumber}";

                    // Indent according to heading level (if level information were available).
                    string indent = new string(' ', (level - 1) * 4);
                    tocBuilder.AppendLine($"{indent}<li><a href=\"{anchor}\">{headingText}</a></li>");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while extracting headings for TOC: {ex.Message}");
            // Continue without TOC if extraction fails.
        }

        tocBuilder.AppendLine("</ul>");
        tocBuilder.AppendLine("</nav>");

        // ---------- Insert TOC into the generated HTML ----------
        try
        {
            string htmlContent = File.ReadAllText(htmlPath);
            // Find the opening <body> tag (case‑insensitive) and insert the TOC right after it.
            int bodyIndex = htmlContent.IndexOf("<body", StringComparison.OrdinalIgnoreCase);
            if (bodyIndex >= 0)
            {
                // Find the closing '>' of the <body ...> tag.
                int bodyClose = htmlContent.IndexOf('>', bodyIndex);
                if (bodyClose >= 0)
                {
                    int insertPos = bodyClose + 1;
                    string before = htmlContent.Substring(0, insertPos);
                    string after = htmlContent.Substring(insertPos);
                    string finalHtml = before + Environment.NewLine + tocBuilder.ToString() + Environment.NewLine + after;
                    File.WriteAllText(finalHtmlPath, finalHtml, Encoding.UTF8);
                    Console.WriteLine($"HTML with TOC saved to '{finalHtmlPath}'.");
                }
                else
                {
                    // Fallback: prepend TOC at the very beginning.
                    File.WriteAllText(finalHtmlPath, tocBuilder.ToString() + Environment.NewLine + htmlContent, Encoding.UTF8);
                    Console.WriteLine($"HTML with TOC saved to '{finalHtmlPath}'. (Inserted at file start)");
                }
            }
            else
            {
                // No <body> tag found; prepend TOC.
                File.WriteAllText(finalHtmlPath, tocBuilder.ToString() + Environment.NewLine + htmlContent, Encoding.UTF8);
                Console.WriteLine($"HTML with TOC saved to '{finalHtmlPath}'. (No <body> tag detected)");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while inserting TOC into HTML: {ex.Message}");
        }
    }
}
