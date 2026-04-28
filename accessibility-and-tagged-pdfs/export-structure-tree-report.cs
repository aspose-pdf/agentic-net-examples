using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class ExportStructureTreeReport
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";          // Source PDF
        const string xmlPath      = "structure.xml";      // Intermediate XML file
        const string xsltPath     = "report.xslt";        // XSLT stylesheet for the report
        const string reportPath   = "report.html";        // Final report (HTML in this example)

        // Verify source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Export the document (including its structure tree) to XML
                pdfDocument.SaveXml(xmlPath);
                Console.WriteLine($"Structure tree exported to XML: {xmlPath}");
            }

            // Transform the exported XML using the provided XSLT
            XslCompiledTransform transformer = new XslCompiledTransform();
            transformer.Load(xsltPath); // Load the XSLT stylesheet

            // Perform the transformation
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            using (XmlReader xmlReader = XmlReader.Create(xmlStream))
            using (FileStream reportStream = File.Create(reportPath))
            {
                transformer.Transform(xmlReader, null, reportStream);
            }

            Console.WriteLine($"Report generated: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}