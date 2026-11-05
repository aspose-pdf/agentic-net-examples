using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths and settings
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "HtmlOutput";
        const string baseFileName = "document.html"; // base name for split HTML files
        const string sitemapFileName = "sitemap.xml";
        const string baseUrl = "https://example.com/"; // Change to your site URL

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF and convert to HTML (split into pages)
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                string htmlOutputPath = Path.Combine(outputFolder, baseFileName);
                pdfDoc.Save(htmlOutputPath, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only)
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

        // Create sitemap.xml
        string sitemapPath = Path.Combine(outputFolder, sitemapFileName);
        try
        {
            using (XmlWriter writer = XmlWriter.Create(sitemapPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                foreach (string filePath in htmlFiles)
                {
                    string fileName = Path.GetFileName(filePath);
                    string url = $"{baseUrl}{fileName}";

                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", url);
                    writer.WriteEndElement(); // url
                }

                writer.WriteEndElement(); // urlset
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Sitemap generated at: {sitemapPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error generating sitemap: {ex.Message}");
        }
    }
}