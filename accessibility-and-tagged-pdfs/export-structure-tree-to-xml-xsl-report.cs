using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;               // Aspose.Pdf namespace contains Document, XmlSaveOptions

class ExportStructureTree
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";
        const string xmlPath      = "structure.xml";
        const string xsltPath     = "report.xslt";
        const string reportPath   = "report.html";

        // Ensure source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF document
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // 2. Export the structure tree (logical structure) to XML
            //    Use XmlSaveOptions as required by the Aspose.Pdf API.
            // -----------------------------------------------------------------
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            pdfDoc.Save(xmlPath, xmlOptions);
        }

        // Verify that the XML was created
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Failed to create XML file: {xmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 3. Transform the exported XML using XSLT to produce a report
        // -----------------------------------------------------------------
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Load the XSLT stylesheet
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            // Prepare the XML source
            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            {
                // Output the transformed result to a file (HTML in this example)
                using (FileStream outStream = new FileStream(reportPath, FileMode.Create, FileAccess.Write))
                using (XmlWriter writer = XmlWriter.Create(outStream, xslt.OutputSettings))
                {
                    xslt.Transform(xmlReader, writer);
                }
            }

            Console.WriteLine($"Report generated successfully: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"XSLT transformation error: {ex.Message}");
        }
    }
}