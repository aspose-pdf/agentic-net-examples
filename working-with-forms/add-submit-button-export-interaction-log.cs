using System;
using System.IO;
using System.Xml;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "filled_form.pdf";
        const string xmlLogPath = "form_interaction_log.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Verify that the document contains an AcroForm with fields
                if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
                {
                    Console.WriteLine("No form fields detected in the PDF.");
                }
                else
                {
                    // Export the form field values (interaction log) to an XML file
                    ExportFormFieldsToXml(doc, xmlLogPath);
                    Console.WriteLine($"Form interaction log exported to '{xmlLogPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void ExportFormFieldsToXml(Document doc, string xmlPath)
    {
        // Use XmlWriter to create a simple XML representation of the form data
        using (var writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("FormFields");

            foreach (Field field in doc.Form.Fields)
            {
                writer.WriteStartElement("Field");
                writer.WriteAttributeString("Name", field.Name ?? field.FullName ?? string.Empty);
                writer.WriteAttributeString("Value", field.Value?.ToString() ?? string.Empty);
                writer.WriteEndElement(); // </Field>
            }

            writer.WriteEndElement(); // </FormFields>
            writer.WriteEndDocument();
        }
    }
}
