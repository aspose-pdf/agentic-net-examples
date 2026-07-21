using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class PdfToHtmlSitemap
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Directory where HTML pages and sitemap will be saved
        const string outputDir = "HtmlOutput";

        // Base URL that will be used in the sitemap (adjust to your site)
        const string baseUrl = "https://www.example.com/html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // List to collect generated HTML file names
        List<string> generatedHtmlFiles = new List<string>();

        // Configure HTML conversion options
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Generate one HTML file per PDF page
            SplitIntoPages = true,

            // Custom strategy to save each generated HTML page and record its name
            CustomHtmlSavingStrategy = (Aspose.Pdf.HtmlSaveOptions.HtmlPageMarkupSavingInfo htmlInfo) =>
            {
                // Build a file name like "page_1.html", "page_2.html", etc.
                string fileName = $"page_{htmlInfo.HtmlHostPageNumber}.html";
                string fullPath = Path.Combine(outputDir, fileName);

                // Ensure the content stream is at the beginning
                if (htmlInfo.ContentStream.CanSeek)
                    htmlInfo.ContentStream.Position = 0;

                // Write the HTML markup to the file
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    htmlInfo.ContentStream.CopyTo(fs);
                }

                // Record the file name for sitemap generation
                generatedHtmlFiles.Add(fileName);
            }
        };

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Convert PDF to HTML using the configured options
                pdfDocument.Save(Path.Combine(outputDir, "placeholder.html"), htmlOptions);
                // The actual HTML files are created by the custom strategy above
            }

            // Build sitemap.xml according to the sitemaps.org schema
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement urlset = new XElement(ns + "urlset");

            foreach (string htmlFile in generatedHtmlFiles)
            {
                string loc = $"{baseUrl}/{htmlFile}";
                urlset.Add(
                    new XElement(ns + "url",
                        new XElement(ns + "loc", loc)
                    )
                );
            }

            XDocument sitemapDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                urlset
            );

            // Save the sitemap.xml file
            string sitemapPath = Path.Combine(outputDir, "sitemap.xml");
            sitemapDoc.Save(sitemapPath);

            Console.WriteLine($"Conversion completed. HTML pages and sitemap saved to '{outputDir}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}