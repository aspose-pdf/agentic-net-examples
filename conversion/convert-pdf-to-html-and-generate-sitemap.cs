using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Desired HTML output file
        const string outputHtml = "output.html";
        // Sitemap XML file to be created
        const string sitemapPath = "sitemap.xml";
        // Base URL used in the sitemap entries (adjust to your site)
        const string baseUrl = "https://www.example.com/";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // --------------------------------------------------------------------
        // Convert PDF to HTML – must use HtmlSaveOptions explicitly
        // --------------------------------------------------------------------
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

        // Example of using a specific markup generation mode (optional)
        // htmlOptions.HtmlMarkupGenerationMode = Aspose.Pdf.HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

        // If you need one HTML file per PDF page, enable SplitIntoPages:
        // htmlOptions.SplitIntoPages = true;

        // Load the PDF document (creation) and save as HTML (saving)
        using (Document pdfDoc = new Document(inputPdf))
        {
            pdfDoc.Save(outputHtml, htmlOptions);
        }

        // --------------------------------------------------------------------
        // Generate a simple sitemap.xml that lists the produced HTML page(s)
        // --------------------------------------------------------------------
        using (XmlWriter writer = XmlWriter.Create(sitemapPath, new XmlWriterSettings { Indent = true }))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            // Single HTML page entry (add more <url> elements if SplitIntoPages is true)
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", $"{baseUrl}{Path.GetFileName(outputHtml)}");
            writer.WriteEndElement(); // </url>

            writer.WriteEndElement(); // </urlset>
            writer.WriteEndDocument();
        }

        Console.WriteLine($"HTML saved to '{outputHtml}'.");
        Console.WriteLine($"Sitemap generated at '{sitemapPath}'.");
    }
}