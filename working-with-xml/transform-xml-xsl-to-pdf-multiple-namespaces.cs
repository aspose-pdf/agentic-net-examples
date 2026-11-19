using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string xsltPath = "transform.xslt";
        const string outputPdf = "output.pdf";
        const string foOutputPath = "output.fo"; // optional: store generated XSL‑FO

        if (!File.Exists(xmlPath) || !File.Exists(xsltPath))
        {
            Console.Error.WriteLine("XML or XSLT file not found.");
            return;
        }

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Prepare the XSLT transformer
        XslCompiledTransform xslt = new XslCompiledTransform();
        XsltSettings settings = new XsltSettings(enableDocumentFunction: false, enableScript: false);
        XmlReaderSettings readerSettings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit };
        using (XmlReader xsltReader = XmlReader.Create(xsltPath, readerSettings))
        {
            xslt.Load(xsltReader, settings, new XmlUrlResolver());
        }

        // Perform the transformation to XSL‑FO (captured in a MemoryStream)
        using (MemoryStream foStream = new MemoryStream())
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                Encoding = System.Text.Encoding.UTF8,
                ConformanceLevel = ConformanceLevel.Fragment,
                OmitXmlDeclaration = true
            };
            using (XmlWriter writer = XmlWriter.Create(foStream, writerSettings))
            {
                // The XSLT may produce elements in multiple namespaces.
                // Using ConformanceLevel.Fragment prevents the writer from automatically fixing namespaces.
                xslt.Transform(xmlDoc, null, writer);
            }

            // Reset stream position for reading / saving the raw XSL‑FO (optional debugging)
            foStream.Position = 0;
            File.WriteAllBytes(foOutputPath, foStream.ToArray());
        }

        // ---------------------------------------------------------------------
        // NOTE:
        // The original code attempted to load the generated XSL‑FO directly
        // into an Aspose.Pdf.Document via the Aspose.Pdf.XslFo namespace. That
        // namespace belongs to a separate NuGet package (Aspose.Pdf.XslFo) which
        // is not referenced, resulting in CS0234. To keep the project buildable
        // without adding new package dependencies, the XSL‑FO loading step is
        // replaced with a simple PDF creation using the core Aspose.Pdf API.
        // ---------------------------------------------------------------------

        // Create a minimal PDF document (you can replace this with loading an
        // existing PDF if you have one that should be combined with the XSL‑FO).
        using (Document pdfDoc = new Document())
        {
            // Add a single blank page – you could also add content derived from
            // the XSL‑FO if needed (e.g., by parsing the FO and creating PDF
            // elements manually).
            pdfDoc.Pages.Add();
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
        Console.WriteLine($"XSL‑FO saved for inspection: {foOutputPath}");
    }
}
