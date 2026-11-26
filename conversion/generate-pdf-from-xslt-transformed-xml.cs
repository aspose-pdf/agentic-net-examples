using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath = "source.xml";
        const string xsltPath = "transform.xslt";
        const string pdfOutputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // Transform the source XML using the XSLT stylesheet
        string transformedXml;
        var xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);
        using (var stringWriter = new StringWriter())
        using (var xmlReader = XmlReader.Create(xmlInputPath))
        {
            xslt.Transform(xmlReader, null, stringWriter);
            transformedXml = stringWriter.ToString();
        }

        // Create a PDF document, bind the XML string, and save the result
        using (var pdfDoc = new Document())
        {
            // BindXml expects the XML content as a string, not an XmlDocument
            pdfDoc.BindXml(transformedXml);
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{pdfOutputPath}'.");
    }
}
