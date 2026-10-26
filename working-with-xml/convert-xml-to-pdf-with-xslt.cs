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
        const string outputPdf = "output.pdf";

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

        // Transform the XML with the XSLT, producing XSL‑FO in a memory stream
        using (MemoryStream foStream = new MemoryStream())
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            using (XmlWriter foWriter = XmlWriter.Create(foStream, xslt.OutputSettings))
            {
                xslt.Transform(xmlReader, foWriter);
            }

            // Reset the stream position before handing it to Aspose.Pdf
            foStream.Position = 0;

            // Load the XSL‑FO and convert it to PDF using core Aspose.Pdf API only
            var loadOptions = new XslFoLoadOptions();
            using (Document pdfDoc = new Document(foStream, loadOptions))
            {
                pdfDoc.Save(outputPdf);
            }
        }

        // Ensure all native resources used by Aspose.Pdf are released before the process ends.
        // This helps avoid the CS2012 file‑lock error when the project is rebuilt immediately.
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine($"PDF created: {outputPdf}");
    }
}
