using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";      // source PDF
        const string xmlPath      = "structure.xml";  // intermediate XML
        const string xsltPath     = "report.xslt";    // XSLT stylesheet
        const string reportPath   = "report.html";    // final report

        // Verify required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT not found: {xsltPath}");
            return;
        }

        // ------------------------------------------------------------
        // Export the PDF's structure tree (including logical structure)
        // to an XML file using Aspose.Pdf's XmlSaveOptions.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            XmlSaveOptions xmlOpts = new XmlSaveOptions(); // default options
            pdfDoc.Save(xmlPath, xmlOpts);                 // Save as XML
        }

        // ------------------------------------------------------------
        // Transform the generated XML with the provided XSLT to create
        // a human‑readable report (e.g., HTML).
        // ------------------------------------------------------------
        XslCompiledTransform transformer = new XslCompiledTransform();
        transformer.Load(xsltPath); // load the XSLT stylesheet

        using (FileStream outStream = new FileStream(reportPath, FileMode.Create, FileAccess.Write))
        using (XmlWriter writer = XmlWriter.Create(outStream, transformer.OutputSettings))
        {
            transformer.Transform(xmlPath, writer);
        }

        Console.WriteLine($"Report generated at '{reportPath}'.");
    }
}