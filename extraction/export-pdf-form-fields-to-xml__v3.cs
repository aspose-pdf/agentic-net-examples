using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormB.pdf";
        const string outputXml = "FormB_fields.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create the root XML element
                XElement root = new XElement("FormFields");

                // Enumerate all form fields in the document
                foreach (Field field in doc.Form.Fields)
                {
                    // Field name (partial name) and type
                    string fieldName = field.PartialName;
                    string fieldType = field.GetType().Name;

                    // Extract the field's value as a string
                    string fieldValue = GetFieldValue(doc, field) ?? string.Empty;

                    // Build an XML element for the field
                    XElement fieldElement = new XElement("Field",
                        new XAttribute("Name", fieldName),
                        new XAttribute("Type", fieldType),
                        new XAttribute("Value", fieldValue));

                    root.Add(fieldElement);
                }

                // Save the XML document to the specified file
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xmlDoc.Save(outputXml);
            }

            Console.WriteLine($"Form field data exported to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to retrieve the value of various field types
    private static string GetFieldValue(Document doc, Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked ? "Checked" : "Unchecked";
            case RadioButtonOptionField _:
                // RadioButtonOptionField does not expose a Checked property.
                // The selected option is stored on the parent RadioButtonField (the group).
                // Retrieve the group by the field's full name and read its Selected index.
                var group = doc.Form[field.FullName] as RadioButtonField;
                if (group != null && group.Selected > 0 && group.Options.Count >= group.Selected)
                {
                    // Options are 1‑based; the collection is 0‑based.
                    var selectedOption = group.Options[group.Selected - 1];
                    return selectedOption.Name;
                }
                return string.Empty;
            case ListBoxField list:
                // ListBox may have multiple selected items
                return string.Join(", ", list.SelectedItems);
            case ComboBoxField combo:
                // ComboBox stores the selected option's value in the Value property.
                return combo.Value;
            case SignatureField sig:
                return sig.Signature != null ? "Signed" : "Unsigned";
            default:
                // Attempt to read a generic 'Value' property via reflection
                var prop = field.GetType().GetProperty("Value");
                if (prop != null)
                {
                    var val = prop.GetValue(field);
                    return val?.ToString();
                }
                return null;
        }
    }
}
