using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using System.Xml;
using System.Xml.Xsl;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string xmlPath    = "structure.xml"; // intermediate XML export
        const string xsltPath   = "report.xslt";   // XSLT stylesheet
        const string reportPath = "report.html";   // final report

        // Verify required files exist
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

        // Load the PDF and export its structure tree (and full PDF content) to XML
        using (Document doc = new Document(inputPdf))
        {
            // XmlSaveOptions forces XML output regardless of file extension
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            doc.Save(xmlPath, xmlOpts);
        }

        // Apply the XSLT transformation to the exported XML to create the report
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        using (XmlReader reader = XmlReader.Create(xmlPath))
        using (XmlWriter writer = XmlWriter.Create(reportPath, xslt.OutputSettings))
        {
            xslt.Transform(reader, writer);
        }

        Console.WriteLine($"Report generated at '{reportPath}'.");
    }
}