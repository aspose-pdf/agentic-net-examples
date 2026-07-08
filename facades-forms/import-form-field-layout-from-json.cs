using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for FieldType enum

// Define a POCO that matches the JSON structure describing each field.
public class FieldDefinition
{
    public string Name { get; set; }          // Fully qualified field name
    public string Type { get; set; }          // e.g. "Text", "CheckBox", "Radio", "ComboBox"
    public int PageNumber { get; set; }       // 1‑based page index
    public float Llx { get; set; }            // Lower‑left X
    public float Lly { get; set; }            // Lower‑left Y
    public float Urx { get; set; }            // Upper‑right X
    public float Ury { get; set; }            // Upper‑right Y
}

class Program
{
    static void Main()
    {
        const string jsonPath   = "fieldLayout.json";   // JSON describing fields
        const string outputPath = "output.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Deserialize the JSON into an array of field definitions.
        FieldDefinition[] fields;
        using (FileStream jsonStream = File.OpenRead(jsonPath))
        {
            fields = JsonSerializer.Deserialize<FieldDefinition[]>(jsonStream);
        }

        // Create a new blank PDF document with a single page.
        using (Document doc = new Document())
        {
            // Ensure at least one page exists.
            doc.Pages.Add();

            // Use FormEditor (Facade) to add fields based on the JSON layout.
            using (FormEditor formEditor = new FormEditor(doc))
            {
                foreach (var f in fields)
                {
                    // Map string type to the corresponding FieldType enum.
                    FieldType fieldType = MapFieldType(f.Type);

                    // Add the field to the specified page and rectangle.
                    // The AddField method returns true on success; ignore the return value here.
                    formEditor.AddField(
                        fieldType,
                        f.Name,
                        f.PageNumber,
                        f.Llx,
                        f.Lly,
                        f.Urx,
                        f.Ury);
                }

                // Save the modified document via the FormEditor facade.
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }

    // Helper to convert a textual field type into the Aspose.Pdf.Facades.FieldType enum.
    private static FieldType MapFieldType(string typeName)
    {
        return typeName?.ToLowerInvariant() switch
        {
            "text"       => FieldType.Text,
            "checkbox"   => FieldType.CheckBox,
            "radio"      => FieldType.Radio,
            "combobox"   => FieldType.ComboBox,
            "listbox"    => FieldType.ListBox,
            "pushbutton" => FieldType.PushButton,
            "signature"  => FieldType.Signature,
            _            => FieldType.Text   // Default fallback
        };
    }
}