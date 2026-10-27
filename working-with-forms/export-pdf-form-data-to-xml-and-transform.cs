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
        const string pdfPath = "input_form.pdf";      // Source PDF containing form fields
        const string xmlPath = "form_data.xml";       // Intermediate XML file with exported data
        const string xsltPath = "report.xslt";        // XSLT stylesheet defining the custom report
        const string reportPath = "custom_report.html"; // Final report output (can be any format)

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

        // -----------------------------------------------------------------
        // 1. Load the PDF and export its form fields to an XML file.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // The Form class does not expose an ExportToXml method in the core API.
            // We therefore serialize the form fields manually.
            using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FormData");

                if (pdfDoc.Form != null && pdfDoc.Form.Fields != null)
                {
                    foreach (Field field in pdfDoc.Form.Fields)
                    {
                        // Use the field's full name if the short name is null.
                        string fieldName = field.Name ?? field.FullName ?? "UnnamedField";
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("name", fieldName);
                        writer.WriteString(fieldValue);
                        writer.WriteEndElement(); // Field
                    }
                }

                writer.WriteEndElement(); // FormData
                writer.WriteEndDocument();
            }
        }

        // -----------------------------------------------------------------
        // 2. Transform the exported XML using the provided XSLT stylesheet.
        // -----------------------------------------------------------------
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        using (FileStream xmlStream = File.OpenRead(xmlPath))
        using (FileStream resultStream = File.Create(reportPath))
        using (XmlReader xmlReader = XmlReader.Create(xmlStream))
        using (XmlWriter xmlWriter = XmlWriter.Create(resultStream, xslt.OutputSettings))
        {
            xslt.Transform(xmlReader, xmlWriter);
        }

        Console.WriteLine($"Report generated successfully at '{reportPath}'.");
    }
}
