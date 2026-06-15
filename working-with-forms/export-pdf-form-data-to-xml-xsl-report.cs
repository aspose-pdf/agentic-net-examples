using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf; // Core PDF API – contains Document, XmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input_form.pdf";      // PDF containing the form
        const string xmlPath      = "form_data.xml";       // Intermediate XML export
        const string xsltPath     = "report_template.xslt";// XSLT that defines the custom report
        const string reportPath   = "custom_report.html";  // Resulting report

        // Verify source files exist
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

        try
        {
            // -----------------------------------------------------------------
            // 1. Export PDF form data (including all PDF content) to XML.
            // -----------------------------------------------------------------
            using (Document pdfDoc = new Document(pdfPath))
            {
                // XmlSaveOptions lives directly in the Aspose.Pdf namespace.
                XmlSaveOptions xmlSaveOpts = new XmlSaveOptions();
                pdfDoc.Save(xmlPath, xmlSaveOpts);
            }

            // -----------------------------------------------------------------
            // 2. Transform the exported XML using the provided XSLT.
            // -----------------------------------------------------------------
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (FileStream xmlStream   = File.OpenRead(xmlPath))
            using (FileStream resultStream = File.Create(reportPath))
            using (XmlReader xmlReader    = XmlReader.Create(xmlStream))
            using (XmlWriter resultWriter = XmlWriter.Create(resultStream, xslt.OutputSettings))
            {
                xslt.Transform(xmlReader, resultWriter);
            }

            Console.WriteLine($"Form data exported to XML: {xmlPath}");
            Console.WriteLine($"Custom report generated: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
