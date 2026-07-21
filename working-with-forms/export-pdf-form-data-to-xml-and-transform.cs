using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf; // XmlSaveOptions resides in this namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "input.pdf";          // PDF with form fields
        const string xmlPath = "formData.xml";       // Exported XML
        const string xsltPath = "report.xslt";      // XSLT to create custom report
        const string reportPath = "customReport.html"; // Result of transformation

        // Verify input files exist
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
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Export the PDF (including form data) to XML
                XmlSaveOptions xmlSaveOpts = new XmlSaveOptions();
                pdfDoc.Save(xmlPath, xmlSaveOpts);
            }

            // Perform XSLT transformation: XML -> custom report (e.g., HTML)
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath); // Load the XSLT stylesheet

            // Prepare XML reader for the exported XML
            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            {
                // Create writer for the output report
                using (XmlWriter resultWriter = XmlWriter.Create(reportPath, xslt.OutputSettings))
                {
                    xslt.Transform(xmlReader, resultWriter);
                }
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
