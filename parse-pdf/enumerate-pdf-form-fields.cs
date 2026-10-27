using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int targetPage = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            var form = doc.Form;
            if (form == null || form.Fields == null || !form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Build a set of field names that belong to the requested page
            var page = doc.Pages[targetPage];
            var fieldNamesOnPage = new HashSet<string>();
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is WidgetAnnotation widget)
                {
                    if (!string.IsNullOrEmpty(widget.FullName))
                        fieldNamesOnPage.Add(widget.FullName);
                }
            }

            // Enumerate fields and output those that are on the target page
            foreach (Field field in form.Fields)
            {
                if (fieldNamesOnPage.Contains(field.FullName))
                {
                    string name  = field.FullName;
                    string type  = field.GetType().Name;
                    string value = GetFieldValue(field);
                    Console.WriteLine($"Name: {name}, Type: {type}, Value: {value}");
                }
            }
        }
    }

    // Helper method to obtain a string representation of a field's value
    static string GetFieldValue(Field field)
    {
        // Use the runtime type name to avoid compile‑time dependencies on specific field classes
        string typeName = field.GetType().Name;
        switch (typeName)
        {
            case "TextBoxField":
                dynamic txt = field;
                return txt.Value ?? string.Empty;

            case "CheckboxField":
                dynamic chk = field;
                // CheckboxField exposes a Boolean "Checked" property in supported versions
                try { return chk.Checked ? "Checked" : "Unchecked"; }
                catch { return "(unsupported checkbox)"; }

            case "RadioButtonOptionField":
                dynamic rdo = field;
                // Some versions expose "Checked", others expose "Selected" – try both safely
                try { return rdo.Checked ? "Checked" : "Unchecked"; }
                catch
                {
                    try { return rdo.Selected ? "Checked" : "Unchecked"; }
                    catch { return "(unsupported radio)"; }
                }

            case "ListBoxField":
                dynamic lst = field;
                if (lst.SelectedItems != null && lst.SelectedItems.Length > 0)
                    return string.Join(", ", lst.SelectedItems);
                return string.Empty;

            case "ComboBoxField":
                dynamic cmb = field;
                return cmb.Selected ?? string.Empty;

            case "SignatureField":
                dynamic sig = field;
                return sig.Signature != null ? "Signed" : "Unsigned";

            default:
                return "(unsupported field type)";
        }
    }
}
