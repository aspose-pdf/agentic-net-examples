using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf;                     // HtmlSaveOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output HTML file (single page)
        const string outputHtmlPath = "output.html";

        // Output sitemap XML file
        const string sitemapPath = "sitemap.xml";

        // Verify the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Ensure a single HTML file is produced (default behavior)
                    SplitIntoPages = false,

                    // Optional: embed all resources into the HTML to keep a single file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Save the PDF as HTML using the explicit HtmlSaveOptions
                pdfDoc.Save(outputHtmlPath, htmlOptions);
            }

            // -----------------------------------------------------------------
            // Generate a simple sitemap.xml that lists the produced HTML page.
            // -----------------------------------------------------------------
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(sitemapPath, settings))
            {
                // Write the XML declaration and root element
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                // Add a single <url> entry for the generated HTML page
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", Path.GetFullPath(outputHtmlPath));
                writer.WriteEndElement(); // </url>

                writer.WriteEndElement(); // </urlset>
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Conversion complete. HTML saved to '{outputHtmlPath}'.");
            Console.WriteLine($"Sitemap generated at '{sitemapPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}