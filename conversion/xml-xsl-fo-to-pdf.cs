using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string xsltPath = "template.xslt";
        const string outputPdf = "custom_output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Transform the XML using the XSLT to produce XSL‑FO content in memory
            var xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (var ms = new MemoryStream())
            {
                // The XSLT is expected to output XSL‑FO which Aspose.Pdf can load directly
                using (var writer = XmlWriter.Create(ms, xslt.OutputSettings))
                {
                    xslt.Transform(xmlPath, writer);
                }
                ms.Position = 0; // rewind the stream for reading

                // Load the generated XSL‑FO into an Aspose.Pdf Document
                using (Document pdfDoc = new Document(ms))
                {
                    // Save the PDF with the custom file name
                    pdfDoc.Save(outputPdf);
                }
            }

            Console.WriteLine($"PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
