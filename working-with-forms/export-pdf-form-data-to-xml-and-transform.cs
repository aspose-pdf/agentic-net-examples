using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, XmlSaveOptions, etc.)
using System.Xml.Xsl;           // .NET XSLT engine

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "form.pdf";          // PDF containing the form
        const string xmlExportPath  = "formData.xml";      // Intermediate XML file
        const string xsltPath       = "report.xslt";       // XSLT that defines the custom report
        const string reportPath     = "customReport.html"; // Result of the transformation

        // Validate required files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the PDF document and export its form data to XML.
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document(inputPdfPath)) // load PDF
            {
                // XmlSaveOptions is defined in Aspose.Pdf namespace.
                XmlSaveOptions xmlSaveOpts = new XmlSaveOptions();
                pdfDoc.Save(xmlExportPath, xmlSaveOpts); // export to XML
            }

            // ------------------------------------------------------------
            // 2. Transform the exported XML using the provided XSLT.
            // ------------------------------------------------------------
            XslCompiledTransform transformer = new XslCompiledTransform();
            transformer.Load(xsltPath); // load XSLT stylesheet

            // Use the overload that accepts only the input and output file paths –
            // this avoids the overload that expects a Stream for the third argument.
            transformer.Transform(xmlExportPath, reportPath);

            Console.WriteLine($"Custom report generated at: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
