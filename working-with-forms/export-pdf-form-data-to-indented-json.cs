using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "formdata.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a dictionary to hold field names and their values
            var formData = new Dictionary<string, object>();

            // Iterate over all form fields in the document via the Form.Fields collection
            foreach (var field in doc.Form.Fields)
            {
                // Extract the value based on the concrete field type
                switch (field)
                {
                    case TextBoxField txt:
                        formData[txt.PartialName] = txt.Value;
                        break;
                    case CheckboxField chk: // corrected class name (lowercase 'b')
                        formData[chk.PartialName] = chk.Checked;
                        break;
                    case RadioButtonOptionField rad:
                        // For radio buttons the selected value is stored in the Value property
                        formData[rad.PartialName] = rad.Value;
                        break;
                    case ListBoxField list:
                        // ListBox may have multiple selected items
                        formData[list.PartialName] = list.SelectedItems;
                        break;
                    case ComboBoxField combo:
                        formData[combo.PartialName] = combo.Value;
                        break;
                    case SignatureField sig:
                        // Indicate whether a signature is present
                        formData[sig.PartialName] = sig.Signature != null;
                        break;
                    default:
                        // Fallback for any other field types
                        formData[field.PartialName] = field.Value;
                        break;
                }
            }

            // Serialize the dictionary to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(formData, jsonOptions);
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Form data exported to '{outputPath}'.");
    }
}
