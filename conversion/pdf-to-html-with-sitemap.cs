using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;                     // Core API (Document, HtmlSaveOptions)
using Aspose.Pdf.Facades;            // Not needed here but kept for completeness

class PdfToHtmlWithSitemap
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Output HTML file (single page)
        const string htmlPath = "output.html";

        // Sitemap XML file
        const string sitemapPath = "sitemap.xml";

        // Base URL for sitemap entries – adjust to your site domain
        const string baseUrl = "https://www.example.com/";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using the recommended lifecycle pattern)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options – explicit HtmlSaveOptions are required
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Generate a single HTML file (default behavior)
                    SplitIntoPages = false,

                    // Optional: embed all resources into the HTML to keep a single file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Optional: control how raster images are saved
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML using the options above
                pdfDoc.Save(htmlPath, htmlOpts);
            }

            // Build a simple sitemap.xml containing the generated HTML page
            using (XmlWriter writer = XmlWriter.Create(sitemapPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                // Single URL entry for the HTML page
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", $"{baseUrl}{Path.GetFileName(htmlPath)}");
                writer.WriteEndElement(); // </url>

                writer.WriteEndElement(); // </urlset>
                writer.WriteEndDocument();
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
            Console.WriteLine($"Sitemap generated: {sitemapPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}