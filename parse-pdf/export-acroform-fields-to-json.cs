using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Added for form field types

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare a dictionary to hold field name/value pairs
            var fieldData = new Dictionary<string, object>();

            // Ensure the document contains an AcroForm
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    // PartialName is the field's name; Value holds its current value
                    string name = field.PartialName;
                    object value = field.Value;

                    // Some field types (e.g., Checkbox) may return null for unchecked state;
                    // store a boolean representation for clarity
                    if (field is CheckboxField checkBox)
                    {
                        value = checkBox.Checked;
                    }

                    fieldData[name] = value ?? string.Empty;
                }
            }

            // Serialize the dictionary to JSON (indented for readability)
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldData, jsonOptions);

            // Write JSON to the output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"AcroForm data extracted to '{outputJsonPath}'.");
        }
    }
}
