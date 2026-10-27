using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                var fieldValues = new Dictionary<string, object>();

                // Ensure the document contains a form
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        // FullName may be of a non‑string type in some versions; convert safely.
                        string name = field.FullName != null ? field.FullName.ToString() : field.Name;

                        // Extract the field's value in a version‑tolerant way
                        object value = GetFieldValue(field);
                        fieldValues[name] = value;
                    }
                }

                // Serialize the dictionary to a formatted JSON string
                string json = JsonSerializer.Serialize(fieldValues, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(json);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to obtain a field's value based on its concrete type.
    // Uses runtime type inspection and dynamic dispatch to avoid compile‑time
    // dependencies on field classes that may be missing in certain Aspose.Pdf versions.
    private static object GetFieldValue(Field field)
    {
        // Simple fields that reliably expose the Value property
        if (field is TextBoxField || field is SignatureField || field is ButtonField)
        {
            return field.Value;
        }

        // Use the runtime type name for checkbox and radio button option fields.
        // This sidesteps compile‑time missing‑type errors.
        string typeName = field.GetType().Name;

        if (typeName == "CheckBoxField")
        {
            // CheckBoxField typically has a boolean 'Checked' property.
            dynamic cb = field;
            try
            {
                return cb.Checked ? "On" : "Off";
            }
            catch
            {
                // If the property does not exist, fall back to the generic Value.
                return field.Value;
            }
        }

        if (typeName == "RadioButtonOptionField")
        {
            // RadioButtonOptionField also usually provides a 'Checked' property.
            dynamic rb = field;
            try
            {
                return rb.Checked ? rb.Value : null;
            }
            catch
            {
                return field.Value;
            }
        }

        if (field is ListBoxField lb)
        {
            // SelectedItems may be strings or integers depending on the library version.
            var selected = new List<object>();
            foreach (var item in lb.SelectedItems)
                selected.Add(item);

            return selected.Count == 1 ? selected[0] : (object)selected;
        }

        if (field is ComboBoxField combo)
        {
            return combo.Value;
        }

        // Fallback for any other field types.
        return field.Value;
    }
}
