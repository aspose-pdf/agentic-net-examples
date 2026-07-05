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
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "structure.xml";
        const string xsltPath  = "report.xslt";
        const string reportPath = "report.html";

        // Verify input files exist
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
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Save the structure tree (document model) to XML
                XmlSaveOptions xmlOptions = new XmlSaveOptions();
                pdfDoc.Save(xmlPath, xmlOptions);
            }

            // Transform the generated XML using the provided XSLT
            XslCompiledTransform transformer = new XslCompiledTransform();
            transformer.Load(xsltPath);

            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            using (XmlWriter resultWriter = XmlWriter.Create(reportPath, transformer.OutputSettings))
            {
                transformer.Transform(xmlReader, resultWriter);
            }

            Console.WriteLine($"Structure XML saved to '{xmlPath}'.");
            Console.WriteLine($"Report generated at '{reportPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}