using System;
using System.IO;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";        // source PDF
        const string xmlPath    = "structure.xml";   // intermediate XML file
        const string xsltPath   = "report.xslt";    // XSLT stylesheet for the report
        const string reportPath = "report.html";    // final report output

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

        // Load the PDF and export its structure tree to XML
        using (Document doc = new Document(inputPdf))
        {
            // XmlSaveOptions forces a non‑PDF save; without it the file would be saved as PDF
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            doc.Save(xmlPath, xmlOpts);
        }

        // Transform the exported XML with the provided XSLT to produce the report
        XslCompiledTransform transformer = new XslCompiledTransform();
        transformer.Load(xsltPath);

        using (FileStream outStream = File.Create(reportPath))
        {
            // Use the overload that accepts the XML file path (string)
            transformer.Transform(xmlPath, null, outStream);
        }

        Console.WriteLine($"Report generated at '{reportPath}'.");
    }
}
