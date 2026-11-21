using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string pdfPath = "input_form.pdf";
        // Temporary XML file that will hold the exported form data
        const string xmlPath = "form_data.xml";
        // XSLT stylesheet that defines the custom report layout
        const string xsltPath = "report_template.xslt";
        // Final report output (could be HTML, TXT, etc. depending on XSLT)
        const string reportPath = "custom_report.html";

        // Verify required files exist
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

        // ------------------------------------------------------------
        // 1. Load the PDF and export its form fields to an XML file.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            ExportFormDataToXml(pdfDoc, xmlPath);
        }

        // ------------------------------------------------------------
        // 2. Apply the XSLT transformation to the exported XML.
        // ------------------------------------------------------------
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath); // Load the stylesheet

        // Perform the transformation: XML -> custom report
        using (FileStream resultStream = File.Create(reportPath))
        {
            // Use the overload that accepts the XML file path (string).
            xslt.Transform(xmlPath, null, resultStream);
        }

        Console.WriteLine($"Report generated at '{reportPath}'.");
    }

    /// <summary>
    /// Serialises the AcroForm fields of a PDF document to a simple XML structure.
    /// The XML format is custom (FormData → Field elements) because the core
    /// Aspose.Pdf API does not expose an ExportToXml method.
    /// </summary>
    private static void ExportFormDataToXml(Document doc, string outputPath)
    {
        using (XmlWriter writer = XmlWriter.Create(outputPath, new XmlWriterSettings { Indent = true }))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("FormData");

            if (doc?.Form?.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    writer.WriteStartElement("Field");
                    // Prefer the explicit Name; fall back to FullName if needed.
                    writer.WriteAttributeString("Name", field.Name ?? field.FullName);
                    writer.WriteAttributeString("Type", field.GetType().Name);
                    string value = field.Value?.ToString() ?? string.Empty;
                    writer.WriteString(value);
                    writer.WriteEndElement(); // Field
                }
            }

            writer.WriteEndElement(); // FormData
            writer.WriteEndDocument();
        }
    }
}
