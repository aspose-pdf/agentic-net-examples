using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Directory where HTML pages will be written
        const string htmlOutputDir = "html_output";

        // Path for the generated sitemap.xml
        string sitemapPath = Path.Combine(htmlOutputDir, "sitemap.xml");

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(htmlOutputDir);

        // Configure HTML conversion: one HTML file per PDF page
        Aspose.Pdf.HtmlSaveOptions htmlOptions = new Aspose.Pdf.HtmlSaveOptions
        {
            SplitIntoPages = true,               // generate separate HTML files
            Title = "Page {0}"                    // optional title pattern
        };

        // Base name for the generated HTML files.
        // Aspose.Pdf will create page.html, page_1.html, page_2.html, ...
        string baseHtmlPath = Path.Combine(htmlOutputDir, "page.html");

        // Convert PDF to HTML
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(baseHtmlPath, htmlOptions);
        }

        // Gather all generated HTML files
        string[] htmlFiles = Directory.GetFiles(htmlOutputDir, "*.html");

        // Build sitemap.xml according to the sitemaps protocol
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        XElement urlset = new XElement(ns + "urlset");

        foreach (string htmlFile in htmlFiles)
        {
            // Assuming the site will be hosted at https://www.example.com/
            string fileName = Path.GetFileName(htmlFile);
            string url = $"https://www.example.com/{fileName}";

            XElement urlElement = new XElement(ns + "url",
                new XElement(ns + "loc", url),
                new XElement(ns + "lastmod", File.GetLastWriteTimeUtc(htmlFile).ToString("yyyy-MM-dd"))
            );

            urlset.Add(urlElement);
        }

        XDocument sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlset);
        sitemap.Save(sitemapPath);

        Console.WriteLine($"HTML pages saved to '{htmlOutputDir}'.");
        Console.WriteLine($"Sitemap generated at '{sitemapPath}'.");
    }
}