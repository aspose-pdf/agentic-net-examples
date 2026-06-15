using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class ExportStructureTree
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "input.pdf";          // source PDF
        const string xmlPath = "structure.xml";      // intermediate XML export
        const string xslPath = "report.xsl";         // XSLT stylesheet for the report
        const string reportPath = "report.html";     // final transformed report

        // Verify source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSLT not found: {xslPath}");
            return;
        }

        try
        {
            // ---------- Export structure tree to XML ----------
            using (Document pdfDoc = new Document(pdfPath))
            {
                // XmlSaveOptions resides directly in Aspose.Pdf namespace
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                // Save the document model (including the structure tree) as XML
                pdfDoc.Save(xmlPath, xmlOptions);
            }

            // ---------- Transform XML with XSLT to produce the report ----------
            // Load the XSLT stylesheet
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslPath);

            // Perform the transformation: XML -> report (e.g., HTML)
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            using (FileStream resultStream = File.Create(reportPath))
            using (XmlReader xmlReader = XmlReader.Create(xmlStream))
            {
                xslt.Transform(xmlReader, null, resultStream);
            }

            Console.WriteLine($"Structure tree exported to '{xmlPath}' and transformed to report '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
