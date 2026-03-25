using System;
using System.IO;
using Aspose.Pdf;
using System.Xml;
using System.Xml.Xsl;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlPath = "structure.xml";
        const string xsltPath = "report.xslt";
        const string reportPath = "report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // Export the PDF's logical structure tree to XML
        using (Document pdfDoc = new Document(inputPdf))
        {
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            pdfDoc.Save(xmlPath, xmlOpts);
        }

        // Transform the exported XML using the provided XSLT stylesheet
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        using (FileStream outStream = File.Create(reportPath))
        using (XmlReader reader = XmlReader.Create(xmlStream))
        using (XmlWriter writer = XmlWriter.Create(outStream, xslt.OutputSettings))
        {
            xslt.Transform(reader, writer);
        }

        Console.WriteLine($"Report generated at '{reportPath}'.");
    }
}