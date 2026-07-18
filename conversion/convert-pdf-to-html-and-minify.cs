using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Directory where the HTML file(s) will be saved
        const string outputDir = "HtmlOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Path for the generated HTML file (single file output)
        string htmlPath = Path.Combine(outputDir, "output.html");

        try
        {
            // Load the PDF document and optimize it for web delivery
            using (Document doc = new Document(pdfPath))
            {
                // Linearize the PDF to improve loading speed
                doc.Optimize();

                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all resources (images, CSS, fonts) into the HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Optional: generate a fixed‑layout HTML (set to false for flow layout)
                    FixedLayout = false
                };

                // Convert PDF to HTML
                doc.Save(htmlPath, htmlOptions);
            }

            // Minify the generated HTML to reduce size and improve loading speed
            string htmlContent = File.ReadAllText(htmlPath);
            string minifiedHtml = MinifyHtml(htmlContent);
            File.WriteAllText(htmlPath, minifiedHtml);

            Console.WriteLine("PDF successfully converted to HTML and minified.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple HTML minification: collapse whitespace and remove spaces between tags
    static string MinifyHtml(string html)
    {
        // Replace multiple whitespace characters with a single space
        string collapsed = Regex.Replace(html, @"\s+", " ");

        // Remove spaces between closing and opening tags
        string withoutTagSpaces = Regex.Replace(collapsed, @">\s+<", "><");

        return withoutTagSpaces.Trim();
    }
}