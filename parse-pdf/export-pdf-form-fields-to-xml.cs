using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output XML file that will contain the exported form data
        const string outputXmlPath = "form_fields.xml";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a FileStream for the XML output (overwrite if it exists)
            using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            using (XmlWriter writer = XmlWriter.Create(xmlStream, new XmlWriterSettings { Indent = true }))
            {
                // Begin XML document
                writer.WriteStartDocument();
                writer.WriteStartElement("FormFields");

                // Iterate over all form fields and write their data to XML
                foreach (Field field in pdfDocument.Form.Fields)
                {
                    writer.WriteStartElement("Field");
                    writer.WriteAttributeString("Name", field.PartialName);
                    writer.WriteAttributeString("Type", field.GetType().Name);

                    string value = GetFieldValue(field);
                    writer.WriteElementString("Value", value ?? string.Empty);

                    writer.WriteEndElement(); // </Field>
                }

                writer.WriteEndElement(); // </FormFields>
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Form fields exported to XML: {outputXmlPath}");
    }

    // Helper method to extract a string representation of a field's value
    private static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                // CheckboxField exposes a Boolean "Checked" property
                return chk.Checked ? "true" : "false";
            case RadioButtonOptionField rad:
                // Use dynamic to avoid compile‑time binding to the "Checked" property (some versions expose it, some do not)
                dynamic dRad = rad;
                try
                {
                    return dRad.Checked ? "true" : "false";
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    // Fallback: return the field's generic Value if available
                    var valProp = rad.GetType().GetProperty("Value");
                    return valProp?.GetValue(rad) as string ?? string.Empty;
                }
            case ListBoxField list:
                return string.Join(",", list.SelectedItems);
            case ComboBoxField combo:
                return combo.Value;
            case SignatureField sig:
                // Signature fields do not have a textual value; return the field name
                return sig.PartialName;
            default:
                // Generic fallback – try to read a "Value" property if it exists
                var prop = field.GetType().GetProperty("Value");
                if (prop != null)
                {
                    var val = prop.GetValue(field) as string;
                    return val ?? string.Empty;
                }
                return string.Empty;
        }
    }
}
