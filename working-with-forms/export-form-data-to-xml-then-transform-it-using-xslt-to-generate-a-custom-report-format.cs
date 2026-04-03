using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        string dataDir      = "YOUR_DATA_DIRECTORY";
        string pdfPath      = Path.Combine(dataDir, "input.pdf");      // source PDF with form fields
        string xmlPath      = Path.Combine(dataDir, "formData.xml");   // intermediate XML export
        string xsltPath     = Path.Combine(dataDir, "report.xslt");    // XSLT that defines the custom report
        string reportPath   = Path.Combine(dataDir, "report.html");    // final report output

        // Validate input files
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
        // 1. Export PDF form data (including AcroForm fields) to XML
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // XmlSaveOptions is the correct way to save a PDF as XML
            XmlSaveOptions xmlSaveOpts = new XmlSaveOptions();
            pdfDoc.Save(xmlPath, xmlSaveOpts);
        }

        // ------------------------------------------------------------
        // 2. Transform the exported XML using the provided XSLT
        // ------------------------------------------------------------
        XslCompiledTransform xslt = new XslCompiledTransform();

        // Load the XSLT file
        xslt.Load(xsltPath);

        // Perform the transformation: XML -> custom report (e.g., HTML)
        using (FileStream xmlStream      = File.OpenRead(xmlPath))
        using (FileStream resultStream   = File.Create(reportPath))
        using (XmlReader xmlReader       = XmlReader.Create(xmlStream))
        using (XmlWriter resultWriter    = XmlWriter.Create(resultStream, xslt.OutputSettings))
        {
            xslt.Transform(xmlReader, resultWriter);
        }

        Console.WriteLine($"Report generated at: {reportPath}");
    }
}