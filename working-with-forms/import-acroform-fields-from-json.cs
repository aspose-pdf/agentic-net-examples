using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Represents a single AcroForm field definition in the JSON schema
    public class FormFieldSchema
    {
        public string? Type { get; set; }          // "TextBox", "CheckBox", "RadioButton"
        public string? Name { get; set; }          // field name (unique identifier)
        public string? Value { get; set; }         // initial value (e.g., text, "On"/"Off")
        public int PageNumber { get; set; }        // 1‑based page index
        public double Llx { get; set; }            // lower‑left X
        public double Lly { get; set; }            // lower‑left Y
        public double Urx { get; set; }            // upper‑right X
        public double Ury { get; set; }            // upper‑right Y
        public string? GroupName { get; set; }     // for radio buttons (optional)
    }

    static void Main()
    {
        const string jsonPath   = "fields.json";   // input JSON schema
        const string outputPath = "generated.pdf"; // output PDF file

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of field definitions
        List<FormFieldSchema>? fields;
        try
        {
            string json = File.ReadAllText(jsonPath);
            fields = JsonSerializer.Deserialize<List<FormFieldSchema>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        if (fields == null)
        {
            Console.Error.WriteLine("No field definitions found in JSON.");
            return;
        }

        // Keep track of radio button groups so we can set the selected option later
        var radioGroups = new Dictionary<string, RadioButtonField>();
        var radioGroupOptions = new Dictionary<string, List<string>>();

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Ensure at least one page exists (AcroForm fields need a page)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Process each field definition
            foreach (FormFieldSchema field in fields)
            {
                // Guard against null Type or Name
                if (string.IsNullOrWhiteSpace(field.Type) || string.IsNullOrWhiteSpace(field.Name))
                {
                    Console.WriteLine("Skipping field with missing Type or Name.");
                    continue;
                }

                // Add missing pages if the requested page number exceeds current count
                while (doc.Pages.Count < field.PageNumber)
                    doc.Pages.Add();

                // 1‑based page indexing (Aspose.Pdf rule)
                Page page = doc.Pages[field.PageNumber];

                // Fully qualified Rectangle to avoid ambiguity (rectangle-disambiguation rule)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    field.Llx, field.Lly, field.Urx, field.Ury);

                switch (field.Type)
                {
                    case "TextBox":
                        // Create a text box field
                        TextBoxField txtField = new TextBoxField(page, rect);
                        txtField.Value = field.Value ?? string.Empty;
                        txtField.PartialName = field.Name!; // set the field name
                        doc.Form.Add(txtField);
                        break;

                    case "CheckBox":
                        // Create a check box field (correct class name is CheckboxField)
                        CheckboxField chkField = new CheckboxField(page, rect);
                        chkField.Checked = string.Equals(field.Value, "On", StringComparison.OrdinalIgnoreCase) ||
                                           string.Equals(field.Value, "True", StringComparison.OrdinalIgnoreCase);
                        chkField.PartialName = field.Name!;
                        doc.Form.Add(chkField);
                        break;

                    case "RadioButton":
                        // Radio buttons are handled as groups. Each entry represents an option.
                        string groupKey = field.GroupName ?? field.Name; // fallback to field name if no group supplied

                        if (!radioGroups.TryGetValue(groupKey, out RadioButtonField groupField))
                        {
                            // Create a new radio button group on the same page
                            groupField = new RadioButtonField(page);
                            groupField.PartialName = groupKey;
                            doc.Form.Add(groupField);
                            radioGroups[groupKey] = groupField;
                            radioGroupOptions[groupKey] = new List<string>();
                        }

                        // Add the option to the group
                        string optionName = field.Name!;
                        groupField.AddOption(optionName);
                        radioGroupOptions[groupKey].Add(optionName);

                        // If the value indicates this option should be selected, remember the index
                        if (string.Equals(field.Value, "On", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(field.Value, "True", StringComparison.OrdinalIgnoreCase))
                        {
                            // Selected is zero‑based index of the option within the group
                            groupField.Selected = radioGroupOptions[groupKey].Count - 1;
                        }
                        break;

                    default:
                        // Unknown field type – ignore or log as needed
                        Console.WriteLine($"Unsupported field type: {field.Type}");
                        break;
                }
            }

            // Save the generated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
