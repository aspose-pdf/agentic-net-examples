using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class FieldLayout
{
    // Mark properties as required so the compiler knows they will be populated by deserialization
    public required string Name { get; set; }          // Field name
    public required string Type { get; set; }          // Field type (e.g., "Text", "CheckBox")
    public int Page { get; set; }                     // 1‑based page number
    public float Llx { get; set; }                    // Lower‑left X
    public float Lly { get; set; }                    // Lower‑left Y
    public float Urx { get; set; }                    // Upper‑right X
    public float Ury { get; set; }                    // Upper‑right Y
}

class Program
{
    static void Main()
    {
        const string jsonPath   = "fieldLayout.json";   // JSON describing fields
        const string outputPath = "output.pdf";         // Resulting PDF

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of field definitions. The result can be null, so we use a nullable variable.
        List<FieldLayout>? fields;
        using (FileStream fs = File.OpenRead(jsonPath))
        {
            fields = JsonSerializer.Deserialize<List<FieldLayout>>(fs);
        }

        if (fields == null || fields.Count == 0)
        {
            Console.Error.WriteLine("No field definitions found in JSON.");
            return;
        }

        // Create a new PDF document and add pages as needed
        using (Document doc = new Document())
        {
            // Ensure at least one page exists (FormEditor requires a page to attach fields)
            doc.Pages.Add();

            // Add additional pages if any field references a higher page number
            int maxPage = 1;
            foreach (var f in fields)
                if (f.Page > maxPage) maxPage = f.Page;

            while (doc.Pages.Count < maxPage)
                doc.Pages.Add();

            // Use FormEditor to add fields to the document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                foreach (var f in fields)
                {
                    // Map string type to Aspose.Pdf.Facades.FieldType
                    FieldType fieldType = MapFieldType(f.Type);

                    // Add the field; AddField returns true on success
                    bool added = formEditor.AddField(
                        fieldType,
                        f.Name,
                        f.Page,
                        f.Llx,
                        f.Lly,
                        f.Urx,
                        f.Ury);

                    if (!added)
                        Console.Error.WriteLine($"Failed to add field '{f.Name}' on page {f.Page}.");
                }

                // Save the modified document
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }

    // Helper to convert a textual type name to the corresponding FieldType enum value.
    // NOTE: Older Aspose.Pdf versions do not expose a RadioButton enum member. In such cases we map
    // "radiobutton" to a CheckBox, which still creates a selectable form field.
    static FieldType MapFieldType(string typeName)
    {
        return typeName?.ToLowerInvariant() switch
        {
            "text"        => FieldType.Text,
            "checkbox"    => FieldType.CheckBox,
            "radiobutton" => FieldType.CheckBox, // Fallback for versions lacking FieldType.RadioButton
            "combobox"    => FieldType.ComboBox,
            "listbox"     => FieldType.ListBox,
            "signature"   => FieldType.Signature,
            "button"      => FieldType.PushButton,
            _              => FieldType.Text   // Default to text field if unknown
        };
    }
}
