using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace AsposePdfFormGenerator
{
    // Simple POCO that matches the expected JSON schema for a form field.
    // Adjust the properties if your JSON schema differs.
    public class FormFieldDefinition
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;          // "text", "checkbox", "radio", "list" …
        public int Page { get; set; } = 1;        // 1‑based page number
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        // Additional optional properties can be added here (e.g., "Value", "Options", …)
    }

    class Program
    {
        static void Main()
        {
            const string jsonPath = "form_schema.json";   // JSON that describes the fields
            const string outputPdf = "generated_form.pdf"; // Resulting PDF file

            // Create an empty PDF document with a single page (more pages will be added on demand).
            using (Document doc = new Document())
            {
                // Ensure at least one page exists – required for the first field.
                doc.Pages.Add();

                // ------------------------------------------------------------
                // Self‑contained sample JSON schema – the sandbox has no external files.
                // ------------------------------------------------------------
                var sampleFields = new List<FormFieldDefinition>
                {
                    new FormFieldDefinition { Name = "FirstName", Type = "text", Page = 1, X = 100, Y = 700, Width = 200, Height = 20 },
                    new FormFieldDefinition { Name = "Subscribe", Type = "checkbox", Page = 1, X = 100, Y = 650, Width = 15, Height = 15 },
                    new FormFieldDefinition { Name = "GenderMale", Type = "radio", Page = 1, X = 100, Y = 600, Width = 15, Height = 15 },
                    new FormFieldDefinition { Name = "Country", Type = "combobox", Page = 1, X = 100, Y = 550, Width = 150, Height = 20 }
                };
                string sampleJson = JsonSerializer.Serialize(sampleFields, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, sampleJson);

                // Read the JSON schema and create the corresponding AcroForm fields.
                AddFormFieldsFromJson(doc, jsonPath);

                // Save the PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with imported form fields saved to '{outputPdf}'.");
        }

        /// <summary>
        /// Parses a JSON file that describes form fields and adds them to the supplied document.
        /// The JSON must be an array of objects that match <see cref="FormFieldDefinition"/>.
        /// </summary>
        private static void AddFormFieldsFromJson(Document doc, string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException($"JSON schema file not found: {jsonFilePath}");

            string json = File.ReadAllText(jsonFilePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<FormFieldDefinition> fields = JsonSerializer.Deserialize<List<FormFieldDefinition>>(json, options) ?? new List<FormFieldDefinition>();

            if (fields.Count == 0)
                return; // nothing to add

            foreach (var fieldDef in fields)
            {
                // Ensure the target page exists – Aspose.Pdf pages are 1‑based.
                while (doc.Pages.Count < fieldDef.Page)
                    doc.Pages.Add();

                Page page = doc.Pages[fieldDef.Page];
                // Rectangle expects lower‑left and upper‑right coordinates.
                var rect = new Aspose.Pdf.Rectangle(
                    fieldDef.X,
                    fieldDef.Y,
                    fieldDef.X + fieldDef.Width,
                    fieldDef.Y + fieldDef.Height);

                // Create the appropriate field based on the "Type" value.
                switch (fieldDef.Type?.ToLowerInvariant())
                {
                    case "text":
                    case "textbox":
                        var txtField = new TextBoxField(page, rect)
                        {
                            PartialName = fieldDef.Name,
                        };
                        doc.Form.Add(txtField, fieldDef.Page);
                        break;

                    case "checkbox":
                        var chkField = new CheckboxField(page, rect)
                        {
                            PartialName = fieldDef.Name,
                        };
                        doc.Form.Add(chkField, fieldDef.Page);
                        break;

                    case "radio":
                        // For radio buttons Aspose.Pdf expects a group name (PartialName) and an option name.
                        // Here we treat the supplied Name as the group name and use the same name for the option.
                        var radioField = new RadioButtonOptionField(page, rect)
                        {
                            PartialName = fieldDef.Name,
                            OptionName = fieldDef.Name // simple scenario – can be extended.
                        };
                        doc.Form.Add(radioField, fieldDef.Page);
                        break;

                    case "list":
                    case "combobox":
                        var combo = new ComboBoxField(page, rect)
                        {
                            PartialName = fieldDef.Name,
                        };
                        // Example of adding items – extend the JSON schema with an "Options" array if needed.
                        // combo.Choices.Add("Item 1");
                        // combo.Choices.Add("Item 2");
                        doc.Form.Add(combo, fieldDef.Page);
                        break;

                    default:
                        Console.WriteLine($"Unsupported field type '{fieldDef.Type}' for field '{fieldDef.Name}'. Skipping.");
                        break;
                }
            }
        }
    }
}
