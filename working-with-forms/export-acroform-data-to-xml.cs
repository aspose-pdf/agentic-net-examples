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
        const string outputXml = "formdata.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Verify that the document contains an AcroForm
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No AcroForm fields found in the document.");
                    return;
                }

                // Create an XML writer for the output file
                using (XmlWriter writer = XmlWriter.Create(outputXml, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("FormData");

                    // Iterate over each form field
                    foreach (Field field in doc.Form)
                    {
                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Name", field.FullName);
                        writer.WriteAttributeString("Type", field.GetType().Name);

                        // Extract the field value based on its concrete type
                        string value = GetFieldValue(field);
                        writer.WriteElementString("Value", value ?? string.Empty);

                        writer.WriteEndElement(); // </Field>
                    }

                    writer.WriteEndElement(); // </FormData>
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Form data exported to '{outputXml}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to obtain a string representation of a field's value
    static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked ? "true" : "false";
            case RadioButtonField rad:
                // RadioButtonField does not expose SelectedItem; use its Value property
                return rad.Value?.ToString() ?? string.Empty;
            case ListBoxField lst:
                return lst.SelectedItems != null ? string.Join(",", lst.SelectedItems) : string.Empty;
            case ComboBoxField cmb:
                return cmb.Value;
            case SignatureField sig:
                // Indicate whether the signature field is signed
                return sig.Signature != null ? "Signed" : "Unsigned";
            default:
                return field.Value?.ToString();
        }
    }
}