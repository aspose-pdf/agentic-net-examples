using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Verify that the document contains a form with fields.
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            // Export the form data directly to a MemoryStream as XML.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                ExportFormDataToXml(pdfDoc.Form, xmlStream);
                xmlStream.Position = 0; // Reset position for reading.

                // Optional: read the XML content from the stream for demonstration purposes.
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XML:");
                    Console.WriteLine(xmlContent);
                }
            }
        }
    }

    /// <summary>
    /// Serialises the fields of an Aspose.Pdf.Forms.Form into a simple XML representation.
    /// The XML format mimics the structure produced by the Facades Form.ExportXml method
    /// (field name/value pairs wrapped in a root <FormData> element).
    /// </summary>
    private static void ExportFormDataToXml(Form form, Stream outputStream)
    {
        using (XmlWriter writer = XmlWriter.Create(outputStream, new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 }))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("FormData");

            foreach (Field field in form.Fields)
            {
                writer.WriteStartElement("Field");
                writer.WriteAttributeString("Name", field.PartialName ?? string.Empty);
                writer.WriteAttributeString("Type", field.GetType().Name);
                writer.WriteString(GetFieldValue(field));
                writer.WriteEndElement(); // Field
            }

            writer.WriteEndElement(); // FormData
            writer.WriteEndDocument();
        }
    }

    // Helper method to extract a string representation of a field's value.
    private static string GetFieldValue(Field field)
    {
        // Most field types expose the generic Value property. Checkbox needs a special case.
        return field switch
        {
            CheckboxField cb => cb.Checked ? "true" : "false",
            _ => field.Value?.ToString() ?? string.Empty
        };
    }
}
