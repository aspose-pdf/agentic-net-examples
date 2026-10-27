using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "HtmlPages";
        const string sitemapFile = "sitemap.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Base HTML file name; Aspose.Pdf will create index.html, index_1.html, etc.
                string baseHtmlPath = Path.Combine(outputFolder, "index.html");
                pdfDoc.Save(baseHtmlPath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF to HTML conversion: {ex.Message}");
            return;
        }

        // Gather all generated HTML files
        string[] htmlFiles = Directory.GetFiles(outputFolder, "*.html", SearchOption.TopDirectoryOnly);

        // Build sitemap.xml
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        XElement urlset = new XElement(ns + "urlset");

        foreach (string filePath in htmlFiles)
        {
            string fileName = Path.GetFileName(filePath);
            string loc = $"./{fileName}"; // Adjust URL as needed for your site
            XElement url = new XElement(ns + "url",
                new XElement(ns + "loc", loc));
            urlset.Add(url);
        }

        XDocument sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlset);
        string sitemapPath = Path.Combine(outputFolder, sitemapFile);
        sitemap.Save(sitemapPath);

        Console.WriteLine($"HTML pages saved to '{outputFolder}'. Sitemap generated at '{sitemapPath}'.");
    }
}