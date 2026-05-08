using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath   = "data.xml";          // XML source containing per‑page header data
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and build a lookup of page number → header text
        var headerLookup = new Dictionary<int, string>();
        try
        {
            XDocument xdoc = XDocument.Load(xmlPath);
            // Expected XML format:
            // <Document>
            //   <Page number="1"><Header>First page header</Header></Page>
            //   <Page number="2"><Header>Second page header</Header></Page>
            //   ...
            // </Document>
            foreach (var pageElem in xdoc.Root?.Elements("Page") ?? new List<XElement>())
            {
                XAttribute numAttr = pageElem.Attribute("number");
                if (numAttr == null) continue; // skip malformed entries
                int pageNum = (int)numAttr;
                string headerText = (string)pageElem.Element("Header") ?? string.Empty;
                headerLookup[pageNum] = headerText;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XML: {ex.Message}");
            return;
        }

        // Load the XML as a PDF document (no XSL transformation)
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Assign a custom header to each page based on the lookup
            foreach (Page page in pdfDoc.Pages)
            {
                if (headerLookup.TryGetValue(page.Number, out string hdr) && !string.IsNullOrEmpty(hdr))
                {
                    HeaderFooter header = new HeaderFooter();
                    // You can style the header here (font, alignment, etc.)
                    TextFragment tf = new TextFragment(hdr);
                    header.Paragraphs.Add(tf);
                    page.Header = header;
                }
            }

            // Ensure any pagination placeholders (e.g., $p, $P) are refreshed
            pdfDoc.Pages.UpdatePagination();

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with dynamic headers saved to '{outputPdf}'.");
    }
}
