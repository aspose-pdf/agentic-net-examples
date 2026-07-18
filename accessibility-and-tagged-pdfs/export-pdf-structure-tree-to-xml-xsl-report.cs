using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class ExportStructureTree
{
    static void Main()
    {
        // Input PDF, XSLT stylesheet and output report paths
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "structure.xml";
        const string xsltPath  = "report.xslt";
        const string reportPath = "report.html";

        // Verify files exist
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
            // Load PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Export the logical structure tree to XML
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                pdfDoc.Save(xmlPath, xmlOptions);
            }

            // Transform the exported XML using the provided XSLT
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            // Prepare XML reader for the saved structure XML
            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            using (XmlWriter resultWriter = XmlWriter.Create(reportPath, xslt.OutputSettings))
            {
                xslt.Transform(xmlReader, resultWriter);
            }

            Console.WriteLine($"Structure tree exported to '{xmlPath}' and transformed to report '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}