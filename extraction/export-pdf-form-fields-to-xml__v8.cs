using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for Field and concrete field types

class Program
{
    static void Main()
    {
        const string pdfPath = "FormG.pdf";
        const string xmlPath = "FormG_fields.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create an XML document to hold the exported field data
            XDocument xmlDoc = new XDocument(new XElement("Fields"));

            // Enumerate all form fields in the document
            foreach (Aspose.Pdf.Forms.Field field in doc.Form.Fields)
            {
                // Retrieve field name
                string fieldName = field.FullName ?? string.Empty;

                // Retrieve field value in a type‑safe way
                string fieldValue = GetFieldValueAsString(field);

                // Add a <Field> element with name and value attributes
                XElement fieldElement = new XElement("Field",
                    new XAttribute("Name", fieldName),
                    new XAttribute("Value", fieldValue));

                xmlDoc.Root.Add(fieldElement);
            }

            // Save the XML to the specified path
            xmlDoc.Save(xmlPath);
        }

        Console.WriteLine($"Form fields exported to '{xmlPath}'.");
    }

    /// <summary>
    /// Returns the value of a form field as a string, handling the most common concrete types.
    /// </summary>
    private static string GetFieldValueAsString(Aspose.Pdf.Forms.Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value?.ToString() ?? string.Empty;
            case CheckboxField chk:
                return chk.Checked.ToString();
            case RadioButtonField rad:
                // Selected is 1‑based index of the chosen option; 0 means none selected
                return rad.Selected > 0 ? rad.Selected.ToString() : string.Empty;
            case ListBoxField lst:
                // ListBox can have multiple selected items; join them with commas
                return lst.SelectedItems != null ? string.Join(",", lst.SelectedItems) : string.Empty;
            case ComboBoxField cmb:
                return cmb.Value?.ToString() ?? string.Empty;
            case SignatureField sig:
                return sig.Signature != null ? "Signed" : string.Empty;
            default:
                // Fallback for any other field types that expose a Value property
                var valueProp = field.GetType().GetProperty("Value");
                if (valueProp != null)
                {
                    var val = valueProp.GetValue(field);
                    return val?.ToString() ?? string.Empty;
                }
                return string.Empty;
        }
    }
}
