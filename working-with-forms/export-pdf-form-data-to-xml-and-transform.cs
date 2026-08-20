using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class ExportFormDataAndTransform
{
    static void Main()
    {
        // Paths for the source PDF, intermediate XML, XSLT stylesheet, and final report.
        const string pdfPath      = "input.pdf";
        const string xmlPath      = "formData.xml";
        const string xsltPath     = "reportTemplate.xslt";
        const string reportPath   = "customReport.html";

        // Ensure the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the XSLT stylesheet exists.
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // STEP 1: Load the PDF document.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // STEP 2: Export the PDF (including form data) to XML.
            // XmlSaveOptions exports the entire PDF structure; form fields are represented in the XML.
            XmlSaveOptions xmlSaveOptions = new XmlSaveOptions();
            pdfDocument.Save(xmlPath, xmlSaveOptions);
        }

        // STEP 3: Transform the exported XML using the provided XSLT to generate the custom report.
        // XslCompiledTransform performs the XSLT transformation.
        XslCompiledTransform transformer = new XslCompiledTransform();

        // Load the XSLT stylesheet.
        using (XmlReader xsltReader = XmlReader.Create(xsltPath))
        {
            transformer.Load(xsltReader);
        }

        // Perform the transformation: XML input -> transformed output (e.g., HTML).
        using (XmlReader xmlReader = XmlReader.Create(xmlPath))
        using (XmlWriter resultWriter = XmlWriter.Create(reportPath, transformer.OutputSettings))
        {
            transformer.Transform(xmlReader, resultWriter);
        }

        Console.WriteLine($"Form data exported to XML: {xmlPath}");
        Console.WriteLine($"Custom report generated: {reportPath}");
    }
}