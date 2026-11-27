using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for form field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "formfields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            var fieldData = new Dictionary<string, object>();

            // Ensure the document contains a form with fields
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    object value = null;

                    // Extract the value based on the concrete field type
                    switch (field)
                    {
                        case TextBoxField txt:
                            value = txt.Value;
                            break;
                        case CheckboxField chk:
                            value = chk.Checked;
                            break;
                        case RadioButtonField rad:
                            // Selected is 1‑based index of the chosen option; 0 means none selected
                            value = rad.Selected;
                            break;
                        case ListBoxField list:
                            // Returns an array of selected indices (1‑based)
                            value = list.SelectedItems;
                            break;
                        case ComboBoxField combo:
                            // The selected value is exposed via the Value property (string).
                            // If you need the 1‑based index you can use combo.Selected.
                            value = combo.Value;
                            break;
                        case SignatureField sig:
                            value = sig.Signature != null ? "Signed" : "Unsigned";
                            break;
                        default:
                            // Generic fallback – many fields expose a Value property
                            value = field.Value;
                            break;
                    }

                    // Use the field's partial name as the JSON key (full name can be used as well)
                    fieldData[field.PartialName] = value;
                }
            }

            // Write the collected field data to JSON using a FileStream
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(fs, fieldData, jsonOptions);
            }
        }

        Console.WriteLine($"Form fields exported to '{outputPath}'.");
    }
}
