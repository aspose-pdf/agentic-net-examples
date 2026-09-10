using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf;                     // HtmlSaveOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Output HTML file (single page)
        const string htmlPath = "output.html";

        // Sitemap XML file to be generated
        const string sitemapPath = "sitemap.xml";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert PDF to HTML
        // -----------------------------------------------------------------
        try
        {
            // Load the PDF document (using the recommended using pattern)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Initialize HTML save options.
                // HtmlSaveOptions resides in Aspose.Pdf namespace (no separate sub‑namespace).
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Optional: generate a single HTML file (default). If you need one HTML per PDF page,
                // set htmlOptions.SplitIntoPages = true; and handle the generated file names accordingly.
                // htmlOptions.SplitIntoPages = false;

                // Save the PDF as HTML. Explicitly passing HtmlSaveOptions ensures the output is HTML,
                // because Document.Save(string) without options always writes PDF regardless of extension.
                pdfDoc.Save(htmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping conversion on this platform.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Generate sitemap.xml listing the HTML page(s)
        // -----------------------------------------------------------------
        try
        {
            // Create the root <urlset> element with the required namespace for sitemaps.
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement urlset = new XElement(ns + "urlset");

            // Add an entry for the generated HTML file.
            // In a real scenario you might need to provide an absolute URL.
            XElement url = new XElement(ns + "url",
                new XElement(ns + "loc", new Uri(Path.GetFullPath(htmlPath)).AbsoluteUri),
                new XElement(ns + "lastmod", DateTime.UtcNow.ToString("yyyy-MM-dd"))
            );

            urlset.Add(url);

            // Build the XDocument and save it.
            XDocument sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlset);
            sitemap.Save(sitemapPath);

            Console.WriteLine($"Sitemap generated: {sitemapPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while creating sitemap: {ex.Message}");
        }
    }
}