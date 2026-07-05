using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF, intermediate XML, XSLT stylesheet, and final report.
        const string pdfPath   = "input_form.pdf";
        const string xmlPath   = "form_data.xml";
        const string xsltPath  = "report.xslt";
        const string reportPath = "custom_report.html";

        // Verify required files exist.
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

        // Load the PDF document containing the form.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Export the PDF (including form fields) to XML using Aspose.Pdf's XmlSaveOptions.
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            pdfDoc.Save(xmlPath, xmlOpts);
        }

        // Transform the exported XML with the XSLT stylesheet to produce the custom report.
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        // Perform the transformation and write the result to the report file.
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        using (XmlReader xmlReader = XmlReader.Create(xmlStream))
        using (FileStream outStream = File.Create(reportPath))
        using (XmlWriter resultWriter = XmlWriter.Create(outStream, xslt.OutputSettings))
        {
            xslt.Transform(xmlReader, resultWriter);
        }

        Console.WriteLine($"Report generated at '{reportPath}'.");
    }
}