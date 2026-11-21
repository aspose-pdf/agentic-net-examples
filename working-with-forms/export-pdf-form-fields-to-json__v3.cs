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
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "form_schema.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a list that will hold the JSON‑serializable field definitions
            var fieldDefinitions = new List<object>();

            // Iterate over all form fields present in the document
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Basic information common to all field types
                var baseInfo = new
                {
                    Name = field.FullName,
                    Type = field.GetType().Name,
                    Value = field.Value?.ToString()
                };

                // Extend the information for specific field types when needed
                object detailedInfo = baseInfo;

                switch (field)
                {
                    case ListBoxField listBox:
                        detailedInfo = new
                        {
                            baseInfo.Name,
                            baseInfo.Type,
                            baseInfo.Value,
                            Options = listBox.Options
                        };
                        break;

                    case ComboBoxField comboBox:
                        detailedInfo = new
                        {
                            baseInfo.Name,
                            baseInfo.Type,
                            baseInfo.Value,
                            Options = comboBox.Options
                        };
                        break;

                    case CheckboxField checkBox:
                        detailedInfo = new
                        {
                            baseInfo.Name,
                            baseInfo.Type,
                            IsChecked = checkBox.Checked,
                            baseInfo.Value
                        };
                        break;

                    case RadioButtonOptionField radioOption:
                        // RadioButtonOptionField does not expose a 'Checked' property.
                        // Use OptionName and the checked‑state name instead.
                        detailedInfo = new
                        {
                            baseInfo.Name,
                            baseInfo.Type,
                            OptionName = radioOption.OptionName,
                            CheckedState = radioOption.GetCheckedStateName(),
                            baseInfo.Value
                        };
                        break;

                    // Add more cases here for other field types if required
                }

                fieldDefinitions.Add(detailedInfo);
            }

            // Serialize the collected definitions to a pretty‑printed JSON string
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldDefinitions, jsonOptions);

            // Write the JSON schema to the output file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Form field schema exported to '{outputJsonPath}'.");
        }
    }
}
