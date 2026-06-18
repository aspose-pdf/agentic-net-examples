using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // XML source
        const string outputPdf = "output.pdf";       // Result PDF

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Parse processing instructions that may affect PDF generation.
        //    Example: <?pdf-page-size width="595" height="842"?>
        // -----------------------------------------------------------------
        double pageWidth = 0;
        double pageHeight = 0;

        using (FileStream fs = File.OpenRead(xmlPath))
        using (XmlReader reader = XmlReader.Create(fs, new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true }))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.ProcessingInstruction && reader.Name.Equals("pdf-page-size", StringComparison.OrdinalIgnoreCase))
                {
                    // Extract attributes from the processing instruction data.
                    // Data format: width="595" height="842"
                    var data = reader.Value;
                    XmlDocument doc = new XmlDocument();
                    // Wrap the data in a dummy element to reuse XML parsing.
                    var wrapper = $"<root {data} />";
                    doc.LoadXml(wrapper);
                    var root = doc.DocumentElement;
                    if (root != null)
                    {
                        if (double.TryParse(root.GetAttribute("width"), out double w))
                            pageWidth = w;
                        if (double.TryParse(root.GetAttribute("height"), out double h))
                            pageHeight = h;
                    }
                }
            }
        }

        // -----------------------------------------------------------------
        // 2. Load the XML into a PDF document using XmlLoadOptions.
        // -----------------------------------------------------------------
        XmlLoadOptions loadOptions = new XmlLoadOptions(); // no XSL provided
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // -----------------------------------------------------------------
            // 3. If a page size was specified via processing instruction,
            //    apply it to all pages.
            // -----------------------------------------------------------------
            if (pageWidth > 0 && pageHeight > 0)
            {
                foreach (Page page in pdfDoc.Pages)
                {
                    // SetPageSize expects width and height in points (1/72 inch).
                    page.SetPageSize(pageWidth, pageHeight);
                }
            }

            // -----------------------------------------------------------------
            // 4. Save the resulting PDF.
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated: {outputPdf}");
    }
}