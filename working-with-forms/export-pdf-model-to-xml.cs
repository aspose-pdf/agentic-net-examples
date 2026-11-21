using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "visibility_state.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // If there are no form fields, nothing to export
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Create an XML writer to store field visibility information
                using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("FieldVisibility");

                    // Iterate over all form fields
                    foreach (Field field in doc.Form)
                    {
                        // Default visibility to true; some field types may not expose a Visible property
                        bool isVisible = true;

                        // Attempt to read a 'Visible' boolean property via reflection if it exists
                        var visibleProp = field.GetType().GetProperty("Visible");
                        if (visibleProp != null && visibleProp.PropertyType == typeof(bool))
                        {
                            isVisible = (bool)visibleProp.GetValue(field);
                        }

                        // Write field information to XML
                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Name", field.FullName);
                        writer.WriteAttributeString("Visible", isVisible.ToString().ToLower());
                        writer.WriteEndElement(); // </Field>
                    }

                    writer.WriteEndElement(); // </FieldVisibility>
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Field visibility state exported to '{xmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}