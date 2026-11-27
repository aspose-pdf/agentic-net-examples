using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormD.pdf";
        const string outputXml = "FormD_fields.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains a form with fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Create the root XML element
            XElement root = new XElement("FormFields");

            // Enumerate each form field directly from the Form collection
            foreach (Field field in doc.Form)
            {
                if (field == null) continue; // safety check

                string fieldName = field.Name; // field identifier
                string fieldType = field.GetType().Name;
                string fieldValue = GetFieldValue(field);

                XElement fieldElement = new XElement("Field",
                    new XAttribute("Name", fieldName ?? string.Empty),
                    new XAttribute("Type", fieldType),
                    new XAttribute("Value", fieldValue ?? string.Empty));

                root.Add(fieldElement);
            }

            // Save the XML document
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xmlDoc.Save(outputXml);
            Console.WriteLine($"Form field data exported to '{outputXml}'.");
        }
    }

    // Helper method to extract the value from various field types
    private static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked ? "true" : "false";
            case RadioButtonField rad:
                if (rad.Selected > 0 && rad.Options != null && rad.Selected <= rad.Options.Count)
                    return rad.Options[rad.Selected - 1].Name;
                return string.Empty;
            case ListBoxField list:
                if (list.SelectedItems != null && list.SelectedItems.Length > 0)
                {
                    var names = new List<string>();
                    foreach (int idx in list.SelectedItems)
                    {
                        int zeroBased = idx - 1;
                        if (zeroBased >= 0 && zeroBased < list.Options.Count)
                            names.Add(list.Options[zeroBased].Name);
                    }
                    return string.Join(",", names);
                }
                return string.Empty;
            case ComboBoxField combo:
                if (combo.Selected > 0 && combo.Options != null && combo.Selected <= combo.Options.Count)
                    return combo.Options[combo.Selected - 1].Name;
                return combo.Value ?? string.Empty;
            case SignatureField sig:
                return sig.Signature != null ? "Signed" : "Unsigned";
            default:
                return null;
        }
    }
}
