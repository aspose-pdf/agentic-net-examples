using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;               // Core PDF API (Document, XmlSaveOptions)
using Aspose.Pdf.Text;          // If needed for other text operations (not used here)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input_form.pdf";          // PDF containing form fields
        const string xmlPath      = "form_data.xml";           // Intermediate XML export
        const string xsltPath     = "report_template.xslt";    // XSLT to generate custom report
        const string reportPath   = "custom_report.html";      // Resulting report (HTML)

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
            // -----------------------------------------------------------------
            // 1. Load the PDF document (lifecycle: create/load)
            // -----------------------------------------------------------------
            using (Document pdfDocument = new Document(pdfPath))
            {
                // -----------------------------------------------------------------
                // 2. Export the PDF (including form data) to XML.
                //    Non‑PDF output requires explicit SaveOptions (rule: save-to-non-pdf-always-use-save-options).
                // -----------------------------------------------------------------
                XmlSaveOptions xmlSaveOptions = new XmlSaveOptions();
                pdfDocument.Save(xmlPath, xmlSaveOptions);
            }

            // -----------------------------------------------------------------
            // 3. Transform the exported XML using XSLT to produce the custom report.
            // -----------------------------------------------------------------
            XslCompiledTransform transformer = new XslCompiledTransform();

            // Load the XSLT stylesheet
            transformer.Load(xsltPath);

            // Perform the transformation: XML -> custom report (HTML in this example)
            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            using (XmlWriter resultWriter = XmlWriter.Create(reportPath, transformer.OutputSettings))
            {
                transformer.Transform(xmlReader, resultWriter);
            }

            Console.WriteLine("Form data exported to XML and transformed successfully.");
            Console.WriteLine($"XML file:   {Path.GetFullPath(xmlPath)}");
            Console.WriteLine($"Report:    {Path.GetFullPath(reportPath)}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}