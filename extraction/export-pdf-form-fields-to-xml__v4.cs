using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Core form field types

class Program
{
    static void Main()
    {
        const string inputPath = "FormC.pdf";
        const string outputPath = "FormC_fields.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Root XML element
                XElement root = new XElement("FormFields");

                // Enumerate all form fields
                foreach (Field field in doc.Form.Fields)
                {
                    // Field name (fallback to short name if FullName is null)
                    string fieldName = field.FullName ?? field.Name;

                    // Extract the field value as a string
                    string fieldValue = GetFieldValueAsString(field);

                    // Create an XML element for the field
                    XElement fieldElement = new XElement("Field",
                        new XAttribute("Name", fieldName),
                        new XAttribute("Type", field.GetType().Name),
                        new XAttribute("Value", fieldValue));

                    root.Add(fieldElement);
                }

                // Save the XML document
                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xmlDoc.Save(outputPath);
            }

            Console.WriteLine($"Form fields exported to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to convert different field types to a string representation
    static string GetFieldValueAsString(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value ?? string.Empty;

            case CheckboxField chk:
                return chk.Checked ? "true" : "false";

            case RadioButtonField rad:
                // RadioButtonField stores the selected option index (1‑based) in Selected.
                // If a value (string) is needed, you can map the index to the option name.
                // Here we simply return the index as string.
                return rad.Selected > 0 ? rad.Selected.ToString() : string.Empty;

            case ListBoxField list:
                // SelectedItems is an int[] of 1‑based indices. Convert to a comma‑separated list.
                if (list.SelectedItems == null || list.SelectedItems.Length == 0)
                    return string.Empty;
                return string.Join(",", list.SelectedItems.Select(i => i.ToString()));

            case ComboBoxField combo:
                // The selected value is exposed via the Value property.
                // If the combo box is empty, Value will be null.
                return combo.Value ?? string.Empty;

            case SignatureField sig:
                // SignatureField does not expose a direct Value property; use the Signature object if present
                return sig.Signature?.ToString() ?? string.Empty;

            default:
                // For any other field types, attempt to use the generic Value property (if available)
                return field.Value?.ToString() ?? string.Empty;
        }
    }
}
