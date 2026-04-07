using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF form, source XML, XSLT stylesheet and output PDF
        const string pdfTemplatePath = "template.pdf";
        const string xmlInputPath    = "data.xml";
        const string xslPath         = "transform.xsl";
        const string outputPdfPath   = "filled.pdf";

        // ---------------------------------------------------------------------
        // Load (or create) the PDF template
        // ---------------------------------------------------------------------
        Document pdfDocument;
        if (File.Exists(pdfTemplatePath))
        {
            pdfDocument = new Document(pdfTemplatePath);
        }
        else
        {
            pdfDocument = new Document();
            pdfDocument.Pages.Add();
            Console.WriteLine($"Template file '{pdfTemplatePath}' not found. A blank PDF was created instead.");
        }

        // ---------------------------------------------------------------------
        // Ensure the XML input exists – if not, create a minimal placeholder
        // ---------------------------------------------------------------------
        if (!File.Exists(xmlInputPath))
        {
            var placeholder = "<?xml version='1.0' encoding='UTF-8'?><root></root>";
            File.WriteAllText(xmlInputPath, placeholder, Encoding.UTF8);
            Console.WriteLine($"XML input file '{xmlInputPath}' not found. A placeholder XML was created.");
        }

        // ---------------------------------------------------------------------
        // Load the XSLT stylesheet – fall back to an identity transform if missing
        // ---------------------------------------------------------------------
        XslCompiledTransform xslt = new XslCompiledTransform();
        if (File.Exists(xslPath))
        {
            xslt.Load(xslPath);
        }
        else
        {
            const string identityXslt = "<?xml version='1.0' encoding='UTF-8'?>" +
                "<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>" +
                "<xsl:output method='xml' indent='yes'/>" +
                "<xsl:template match='@*|node()'>" +
                "<xsl:copy><xsl:apply-templates select='@*|node()'/></xsl:copy>" +
                "</xsl:template>" +
                "</xsl:stylesheet>";
            using (var reader = XmlReader.Create(new StringReader(identityXslt)))
            {
                xslt.Load(reader);
            }
            Console.WriteLine($"XSLT file '{xslPath}' not found. An identity transform was used instead.");
        }

        // ---------------------------------------------------------------------
        // Perform the transformation into a memory stream and load it as XmlDocument
        // ---------------------------------------------------------------------
        XmlDocument transformedXml = new XmlDocument();
        using (MemoryStream ms = new MemoryStream())
        {
            xslt.Transform(xmlInputPath, null, ms);
            ms.Position = 0;
            transformedXml.Load(ms);
        }

        // ---------------------------------------------------------------------
        // Populate PDF form fields with values from the transformed XML
        // ---------------------------------------------------------------------
        foreach (Field field in pdfDocument.Form.Fields)
        {
            XmlNode node = transformedXml.SelectSingleNode($"//{field.FullName}");
            if (node != null)
            {
                field.Value = node.InnerText;
            }
        }

        // ---------------------------------------------------------------------
        // Save the populated PDF
        // ---------------------------------------------------------------------
        pdfDocument.Save(outputPdfPath);
        Console.WriteLine($"PDF form populated and saved to '{outputPdfPath}'.");
    }
}
