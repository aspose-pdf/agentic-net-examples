using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string xsltPath = "transform.xslt";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath) || !File.Exists(xsltPath))
        {
            Console.Error.WriteLine("XML or XSLT file not found.");
            return;
        }

        // Load the XSLT stylesheet
        var xslt = new XslCompiledTransform();
        var settings = new XsltSettings(true, true);
        var resolver = new XmlUrlResolver();
        xslt.Load(xsltPath, settings, resolver);

        // Load the source XML document
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Transform XML to an intermediate HTML (or XSL‑FO) representation.
        // No explicit namespace manager is required – the XSLT itself declares the
        // namespaces it works with. Use the overload that accepts an XsltArgumentList
        // (null in this case) to avoid the overload‑resolution conflict.
        using (var htmlStream = new MemoryStream())
        using (var writer = XmlWriter.Create(htmlStream, xslt.OutputSettings))
        {
            xslt.Transform(xmlDoc, null, writer);
            writer.Flush();
            htmlStream.Position = 0; // Reset stream for reading

            // Load the transformed HTML into an Aspose.Pdf Document
            using (var pdfDoc = new Document(htmlStream, new HtmlLoadOptions()))
            {
                // Save the final PDF
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'.");
    }
}
