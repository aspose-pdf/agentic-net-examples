using System;
using System.IO;
using System.Xml.Xsl;
using Aspose.Pdf;

class XmlToPdfWithNamespaces
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string xslPath = "transform.xsl";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslPath}");
            return;
        }

        // Prepare XSLT arguments – you can also pre‑declare namespaces here if the stylesheet
        // expects them as extension namespaces.
        XsltArgumentList xsltArgs = new XsltArgumentList();

        // Load the XSL‑FO stylesheet.
        using (FileStream xslStream = File.OpenRead(xslPath))
        {
            // XslFoLoadOptions takes the XSL stream via its constructor because the XslStream
            // property is read‑only.
            XslFoLoadOptions loadOptions = new XslFoLoadOptions(xslStream)
            {
                XsltArgumentList = xsltArgs
            };

            // The Document constructor runs the transformation and creates the PDF.
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}
