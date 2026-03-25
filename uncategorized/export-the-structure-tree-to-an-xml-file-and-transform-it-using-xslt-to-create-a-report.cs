using System;
using System.IO;
using Aspose.Pdf;
using System.Xml.Xsl;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlFile = "structure.xml";
        const string xsltFile = "report.xslt";
        const string reportFile = "report.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xsltFile))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltFile}");
            return;
        }

        // Load PDF and export its logical structure tree to XML
        using (Document pdfDoc = new Document(inputPdf))
        {
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            pdfDoc.Save(xmlFile, xmlOpts);
        }

        // Transform the exported XML using the provided XSLT stylesheet
        XslCompiledTransform transformer = new XslCompiledTransform();
        transformer.Load(xsltFile);
        using (FileStream outStream = File.Create(reportFile))
        {
            // Use the overload that accepts the XML file path (string)
            transformer.Transform(xmlFile, null, outStream);
        }

        Console.WriteLine($"Report generated: {reportFile}");
    }
}
