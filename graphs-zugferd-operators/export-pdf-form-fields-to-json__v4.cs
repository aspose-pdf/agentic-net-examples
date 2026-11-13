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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect field names and their extracted values
            var fieldValues = new Dictionary<string, object>();

            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Handle each known field type explicitly
                switch (field)
                {
                    case TextBoxField txt:
                        fieldValues[txt.PartialName] = txt.Value;
                        break;

                    case CheckboxField chk:
                        fieldValues[chk.PartialName] = chk.Checked;
                        break;

                    case RadioButtonOptionField rad:
                        // Radio button value is the selected option name
                        fieldValues[rad.PartialName] = rad.Value;
                        break;

                    case ListBoxField list:
                        // ListBox may have multiple selected items
                        fieldValues[list.PartialName] = list.SelectedItems?.ToArray();
                        break;

                    case ComboBoxField combo:
                        fieldValues[combo.PartialName] = combo.Value;
                        break;

                    case SignatureField sig:
                        // Indicate whether a signature is present
                        fieldValues[sig.PartialName] = sig.Signature != null;
                        break;

                    default:
                        // Fallback: attempt to read a generic Value property via reflection
                        var prop = field.GetType().GetProperty("Value");
                        if (prop != null)
                        {
                            fieldValues[field.PartialName] = prop.GetValue(field);
                        }
                        else
                        {
                            fieldValues[field.PartialName] = null;
                        }
                        break;
                }
            }

            // Convert the dictionary to a formatted JSON string
            string json = JsonSerializer.Serialize(fieldValues, new JsonSerializerOptions { WriteIndented = true });

            // Output the JSON to the console
            Console.WriteLine(json);
        }
    }
}