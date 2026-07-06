using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmlTemplatePath = "headerfooter.xml";
        const string outputPdfPath = "output.pdf";

        // ------------------------------------------------------------
        // Ensure a source PDF exists. If not, create a minimal one.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a single blank page.
                placeholder.Pages.Add();
                placeholder.Save(inputPdfPath);
            }
        }

        // Ensure the XML template exists – create a minimal default if it does not.
        if (!File.Exists(xmlTemplatePath))
        {
            var defaultXml = new XDocument(
                new XElement("Document",
                    new XElement("Header", new XAttribute("page", "1"), "Default Header"),
                    new XElement("Footer", new XAttribute("page", "1"), "Default Footer")
                )
            );
            defaultXml.Save(xmlTemplatePath);
        }

        // Load XML template that defines header/footer text per page.
        // Expected format:
        // <Document>
        //   <Header page="1">Header text for page 1</Header>
        //   <Footer page="1">Footer text for page 1</Footer>
        //   <Header page="2">Header text for page 2</Header>
        //   <Footer page="2">Footer text for page 2</Footer>
        //   ...
        // </Document>
        XDocument xml = XDocument.Load(xmlTemplatePath);

        // Build lookup dictionaries: page number -> text.
        // Use explicit parsing and null‑forgiving operators to satisfy the nullable reference analysis.
        Dictionary<int, string> headerTexts = xml.Root!
            .Elements("Header")
            .Where(e => e.Attribute("page") != null)
            .ToDictionary(
                e => int.Parse(e.Attribute("page")!.Value),
                e => (string?)e.Value ?? string.Empty);

        Dictionary<int, string> footerTexts = xml.Root!
            .Elements("Footer")
            .Where(e => e.Attribute("page") != null)
            .ToDictionary(
                e => int.Parse(e.Attribute("page")!.Value),
                e => (string?)e.Value ?? string.Empty);

        // Open the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Attach a handler to each page's OnBeforePageGenerate event.
            // The handler will set Header and Footer based on the XML data.
            foreach (Page page in pdfDoc.Pages)
            {
                page.OnBeforePageGenerate += (Aspose.Pdf.Page p) =>
                {
                    int pageNumber = p.Number;

                    // Set header if defined for this page.
                    if (headerTexts.TryGetValue(pageNumber, out string? hdr) && !string.IsNullOrEmpty(hdr))
                    {
                        HeaderFooter header = new HeaderFooter();
                        header.Paragraphs.Add(new TextFragment(hdr));
                        p.Header = header;
                    }

                    // Set footer if defined for this page.
                    if (footerTexts.TryGetValue(pageNumber, out string? ftr) && !string.IsNullOrEmpty(ftr))
                    {
                        HeaderFooter footer = new HeaderFooter();
                        footer.Paragraphs.Add(new TextFragment(ftr));
                        p.Footer = footer;
                    }
                };
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with headers and footers saved to '{outputPdfPath}'.");
    }
}
