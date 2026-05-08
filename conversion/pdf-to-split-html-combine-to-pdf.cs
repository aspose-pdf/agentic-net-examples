using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths
        const string inputPdfPath      = "input.pdf";
        const string htmlOutputBase    = "html_pages\\output.html";   // base name for split HTML pages
        const string combinedHtmlPath  = "combined.html";
        const string finalPdfPath      = "final.pdf";

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the directory for split HTML pages exists
        string htmlDir = Path.GetDirectoryName(htmlOutputBase);
        if (string.IsNullOrEmpty(htmlDir))
            htmlDir = ".";
        Directory.CreateDirectory(htmlDir);

        try
        {
            // ------------------------------------------------------------
            // 1. Convert PDF to multiple HTML pages (one per PDF page)
            // ------------------------------------------------------------
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                Aspose.Pdf.HtmlSaveOptions htmlOpts = new Aspose.Pdf.HtmlSaveOptions
                {
                    // Generate one HTML file per PDF page
                    SplitIntoPages = true,
                    // Embed all resources (images, CSS, fonts) into each HTML file
                    PartsEmbeddingMode = Aspose.Pdf.HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Optional: set a title for the generated pages
                    Title = "Converted Pages"
                };

                // Save the PDF as split HTML files. The files will be named:
                // output.html, output_2.html, output_3.html, ...
                pdfDoc.Save(htmlOutputBase, htmlOpts);
            }

            // ------------------------------------------------------------
            // 2. Combine the generated HTML pages into a single HTML file
            // ------------------------------------------------------------
            // Determine the pattern of generated files (first file is output.html,
            // subsequent files have an underscore and page number)
            string baseFileName = Path.GetFileNameWithoutExtension(htmlOutputBase); // "output"
            string baseFilePath = Path.Combine(htmlDir, baseFileName + ".html");

            // Collect all split HTML files in order
            var htmlFiles = Directory.GetFiles(htmlDir, $"{baseFileName}*.html")
                                     .OrderBy(f =>
                                     {
                                         // Extract page number: 0 for the first file, otherwise the number after the underscore
                                         string name = Path.GetFileNameWithoutExtension(f);
                                         int underscore = name.LastIndexOf('_');
                                         if (underscore > 0 && int.TryParse(name.Substring(underscore + 1), out int num))
                                             return num;
                                         return 0; // first file
                                     })
                                     .ToList();

            // Helper to extract the <body> content from an HTML file
            string ExtractBody(string htmlContent)
            {
                const string bodyStartTag = "<body";
                const string bodyEndTag   = "</body>";
                int startIdx = htmlContent.IndexOf(bodyStartTag, StringComparison.OrdinalIgnoreCase);
                if (startIdx < 0) return string.Empty;
                // Find the closing '>' of the opening <body ...> tag
                startIdx = htmlContent.IndexOf('>', startIdx);
                if (startIdx < 0) return string.Empty;
                startIdx++; // move past '>'

                int endIdx = htmlContent.IndexOf(bodyEndTag, startIdx, StringComparison.OrdinalIgnoreCase);
                if (endIdx < 0) return string.Empty;

                return htmlContent.Substring(startIdx, endIdx - startIdx);
            }

            // Build combined HTML
            string combinedHead = string.Empty;
            string combinedBody = string.Empty;

            foreach (string file in htmlFiles)
            {
                string html = File.ReadAllText(file);
                if (string.IsNullOrEmpty(combinedHead))
                {
                    // Capture the <head> section from the first file
                    int headStart = html.IndexOf("<head", StringComparison.OrdinalIgnoreCase);
                    int headEnd   = html.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
                    if (headStart >= 0 && headEnd > headStart)
                    {
                        int headClose = html.IndexOf('>', headStart);
                        if (headClose >= 0 && headClose < headEnd)
                        {
                            combinedHead = html.Substring(headClose + 1, headEnd - headClose - 1);
                        }
                    }
                }

                combinedBody += ExtractBody(html) + "\n";
            }

            // Assemble the final combined HTML document
            string finalHtml = $"<html>\n<head>\n{combinedHead}\n</head>\n<body>\n{combinedBody}\n</body>\n</html>";
            File.WriteAllText(combinedHtmlPath, finalHtml);

            // ------------------------------------------------------------
            // 3. Convert the combined HTML back to PDF
            // ------------------------------------------------------------
            using (Aspose.Pdf.Document htmlDoc = new Aspose.Pdf.Document(combinedHtmlPath, new HtmlLoadOptions()))
            {
                // Save as PDF (default PDF format)
                htmlDoc.Save(finalPdfPath);
            }

            Console.WriteLine("Conversion completed successfully.");
            Console.WriteLine($"HTML pages saved to: {htmlDir}");
            Console.WriteLine($"Combined HTML saved to: {combinedHtmlPath}");
            Console.WriteLine($"Final PDF saved to: {finalPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}