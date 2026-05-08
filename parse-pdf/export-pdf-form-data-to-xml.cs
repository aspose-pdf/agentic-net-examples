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
        // Path to the source PDF containing form fields.
        const string pdfPath = "input.pdf";

        // Ensure the file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Export the form data to a MemoryStream as XML.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                WriteFormDataAsXml(pdfDocument, xmlStream);

                // Reset the stream position to the beginning for reading.
                xmlStream.Position = 0;

                // Optionally, read the XML content as a string (e.g., for display or further processing).
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported Form XML:");
                    Console.WriteLine(xmlContent);
                }
            }
        }
    }

    /// <summary>
    /// Writes the form fields of the supplied PDF document to the provided stream in a simple XML format.
    /// The method does not rely on Aspose.Pdf.Facades (which is prohibited by the task constraints).
    /// </summary>
    /// <param name="doc">The loaded PDF document.</param>
    /// <param name="outputStream">A writable stream that will receive the XML.</param>
    private static void WriteFormDataAsXml(Document doc, Stream outputStream)
    {
        // Use an XmlWriter for proper XML encoding and formatting.
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            Encoding = System.Text.Encoding.UTF8,
            OmitXmlDeclaration = false
        };

        using (XmlWriter writer = XmlWriter.Create(outputStream, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("FormData");

            // Iterate over all fields in the form.
            foreach (Field field in doc.Form.Fields)
            {
                writer.WriteStartElement("Field");
                writer.WriteAttributeString("Name", field.PartialName);
                writer.WriteAttributeString("Type", field.GetType().Name);

                string value = GetFieldValue(field);
                if (!string.IsNullOrEmpty(value))
                {
                    writer.WriteString(value);
                }

                writer.WriteEndElement(); // Field
            }

            writer.WriteEndElement(); // FormData
            writer.WriteEndDocument();
        }
    }

    /// <summary>
    /// Retrieves the string representation of a form field's value.
    /// Handles the most common field types.
    /// </summary>
    private static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked ? "true" : "false";
            case RadioButtonField rad:
                return rad.Value;
            case ListBoxField list:
                // ListBox returns selected indices (int[]). Convert them to strings.
                var selected = list.SelectedItems?.Select(i => i.ToString()) ?? Enumerable.Empty<string>();
                return string.Join(",", selected);
            case ComboBoxField combo:
                return combo.Value;
            case SignatureField sig:
                // Signature fields do not have a textual value; return empty.
                return string.Empty;
            default:
                // For any other field types, attempt to use the generic "Value" property via reflection.
                var prop = field.GetType().GetProperty("Value");
                if (prop != null && prop.PropertyType == typeof(string))
                {
                    return prop.GetValue(field) as string;
                }
                return string.Empty;
        }
    }
}
