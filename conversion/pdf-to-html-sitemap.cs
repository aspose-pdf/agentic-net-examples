using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string pdfPath = "input.pdf";
        string outputDir = "html_output";
        string sitemapPath = "sitemap.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Convert PDF to separate HTML pages
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            SplitIntoPages = true,
            PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
        };

        using (Document pdfDoc = new Document(pdfPath))
        {
            // The "page.html" pattern will be replaced with page numbers by the library
            pdfDoc.Save(Path.Combine(outputDir, "page.html"), htmlOptions);
        }

        // Build sitemap XML listing all generated HTML files
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        XElement urlset = new XElement(ns + "urlset");

        foreach (string htmlFile in Directory.GetFiles(outputDir, "*.html"))
        {
            string fileName = Path.GetFileName(htmlFile);
            XElement url = new XElement(ns + "url",
                new XElement(ns + "loc", fileName)
            );
            urlset.Add(url);
        }

        XDocument sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlset);
        sitemap.Save(sitemapPath);

        Console.WriteLine($"HTML pages saved to '{outputDir}'.");
        Console.WriteLine($"Sitemap generated at '{sitemapPath}'.");
    }
}