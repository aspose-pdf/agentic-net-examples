using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class FormFieldInfo
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool? Required { get; set; }
    public int? MaxLength { get; set; }
    public string? DefaultValue { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_schema.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            List<FormFieldInfo> schema = new List<FormFieldInfo>();

            // Iterate over each form field
            foreach (Field field in doc.Form.Fields)
            {
                var info = new FormFieldInfo
                {
                    Name = field.FullName,
                    Type = field.GetType().Name,
                    // Try to read the "IsRequired" flag via reflection – not all versions expose it directly
                    Required = GetBooleanProperty(field, "IsRequired")
                };

                // Extract type‑specific constraints using reflection so the code compiles against any library version
                string fieldType = field.GetType().Name;
                switch (fieldType)
                {
                    case "TextBoxField":
                        // Default value
                        info.DefaultValue = GetStringProperty(field, "Value");
                        // MaxLength (may be missing in older versions)
                        int? maxLen = GetIntProperty(field, "MaxLength");
                        if (maxLen.HasValue && maxLen.Value > 0)
                            info.MaxLength = maxLen;
                        break;

                    case "CheckBoxField":
                        // Checked flag – map to "true"/"false"
                        bool? isChecked = GetBooleanProperty(field, "Checked");
                        if (isChecked.HasValue)
                            info.DefaultValue = isChecked.Value ? "true" : "false";
                        break;

                    case "RadioButtonOptionField":
                        info.DefaultValue = GetStringProperty(field, "Value");
                        break;

                    case "ListBoxField":
                        // SelectedItems is a collection; join with commas if present
                        var selected = GetEnumerableProperty(field, "SelectedItems");
                        if (selected != null && selected.Cast<object>().Any())
                            info.DefaultValue = string.Join(",", selected.Cast<object>());
                        break;

                    case "ComboBoxField":
                        info.DefaultValue = GetStringProperty(field, "Value");
                        break;

                    // Add other field types as needed
                }

                schema.Add(info);
            }

            // Serialize the schema to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(schema, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Form schema written to '{outputJsonPath}'.");
        }
    }

    // Helper: safely get a string property via reflection
    private static string? GetStringProperty(object obj, string propertyName)
    {
        var prop = obj.GetType().GetProperty(propertyName);
        if (prop != null && prop.PropertyType == typeof(string))
            return prop.GetValue(obj) as string;
        return null;
    }

    // Helper: safely get an int property via reflection
    private static int? GetIntProperty(object obj, string propertyName)
    {
        var prop = obj.GetType().GetProperty(propertyName);
        if (prop != null && (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?)))
            return prop.GetValue(obj) as int?;
        return null;
    }

    // Helper: safely get a bool property via reflection
    private static bool? GetBooleanProperty(object obj, string propertyName)
    {
        var prop = obj.GetType().GetProperty(propertyName);
        if (prop != null && (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?)))
            return prop.GetValue(obj) as bool?;
        return null;
    }

    // Helper: safely get an IEnumerable property via reflection
    private static System.Collections.IEnumerable? GetEnumerableProperty(object obj, string propertyName)
    {
        var prop = obj.GetType().GetProperty(propertyName);
        if (prop != null && typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType))
            return prop.GetValue(obj) as System.Collections.IEnumerable;
        return null;
    }
}
