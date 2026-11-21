using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Model representing a field definition from the JSON file
    private class FieldDefinition
    {
        public string Name { get; set; } = null!;          // field identifier
        public string Type { get; set; } = null!;          // e.g., "TextBox", "CheckBox", "RadioButton", "ComboBox"
        public int Page { get; set; } = 1;                 // 1‑based page number
        public double Llx { get; set; }                    // lower‑left X
        public double Lly { get; set; }                    // lower‑left Y
        public double Urx { get; set; }                    // upper‑right X
        public double Ury { get; set; }                    // upper‑right Y
        public string? Value { get; set; }                 // default value or selected option
        public string[]? Options { get; set; }             // for list/combobox items
    }

    static void Main()
    {
        const string jsonPath   = "fields.json";      // input JSON with field definitions
        const string outputPath = "output.pdf";       // generated PDF

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of field definitions
        List<FieldDefinition> fields;
        try
        {
            string json = File.ReadAllText(jsonPath);
            fields = JsonSerializer.Deserialize<List<FieldDefinition>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<FieldDefinition>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        if (fields.Count == 0)
        {
            Console.WriteLine("No field definitions found in JSON.");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Ensure the document has enough pages for the highest page index used
            int maxPage = fields.Max(f => f.Page);
            for (int i = doc.Pages.Count + 1; i <= maxPage; i++)
                doc.Pages.Add();

            // Helper dictionary for radio‑button groups (group name -> field)
            var radioGroups = new Dictionary<string, RadioButtonField>(StringComparer.OrdinalIgnoreCase);

            foreach (var f in fields)
            {
                // Validate page number
                if (f.Page < 1 || f.Page > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number for field '{f.Name}'. Skipping.");
                    continue;
                }

                // Build the rectangle for the field location
                Rectangle rect = new Rectangle(f.Llx, f.Lly, f.Urx, f.Ury);

                // Dispatch based on field type (case‑insensitive)
                switch (f.Type?.Trim().ToLowerInvariant())
                {
                    case "textbox":
                    case "text":
                        var txt = new TextBoxField(doc.Pages[f.Page], rect)
                        {
                            PartialName = f.Name,
                            Value = f.Value ?? string.Empty
                        };
                        doc.Form.Add(txt, f.Page);
                        break;

                    case "checkbox":
                        bool isChecked = string.Equals(f.Value, "On", StringComparison.OrdinalIgnoreCase) ||
                                         string.Equals(f.Value, "true", StringComparison.OrdinalIgnoreCase);
                        var chk = new CheckboxField(doc.Pages[f.Page], rect)
                        {
                            PartialName = f.Name,
                            Checked = isChecked
                        };
                        doc.Form.Add(chk, f.Page);
                        break;

                    case "radiobutton":
                        // RadioButtonField represents a *group*. We create (or reuse) a group per field name.
                        if (!radioGroups.TryGetValue(f.Name, out var radioGroup))
                        {
                            radioGroup = new RadioButtonField(doc.Pages[f.Page])
                            {
                                PartialName = f.Name
                            };
                            doc.Form.Add(radioGroup, f.Page);
                            radioGroups[f.Name] = radioGroup;
                        }
                        // Add an option – the option name is taken from the Value if supplied, otherwise a generated name.
                        string optionName = f.Value ?? $"Option{radioGroup.Options.Count + 1}";
                        radioGroup.AddOption(optionName, rect);
                        // If the supplied value indicates the option should be selected, set Selected to the newly added index.
                        if (string.Equals(f.Value, "On", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(f.Value, optionName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Options are 1‑based; the newly added option is the last one.
                            radioGroup.Selected = radioGroup.Options.Count; // 1‑based index
                        }
                        break;

                    case "combobox":
                        var combo = new ComboBoxField(doc.Pages[f.Page], rect)
                        {
                            PartialName = f.Name
                        };
                        if (f.Options != null && f.Options.Length > 0)
                        {
                            foreach (var opt in f.Options)
                                combo.AddOption(opt);

                            // Pre‑select if a matching value is supplied
                            if (!string.IsNullOrEmpty(f.Value))
                            {
                                int idx = Array.FindIndex(f.Options, o => o == f.Value);
                                if (idx >= 0)
                                    combo.Selected = idx + 1; // 1‑based
                            }
                        }
                        else if (!string.IsNullOrEmpty(f.Value))
                        {
                            // No explicit options list – add the single value as the only option and select it
                            combo.AddOption(f.Value);
                            combo.Selected = 1;
                        }
                        doc.Form.Add(combo, f.Page);
                        break;

                    case "listbox":
                        var list = new ListBoxField(doc.Pages[f.Page], rect)
                        {
                            PartialName = f.Name
                        };
                        if (f.Options != null && f.Options.Length > 0)
                        {
                            foreach (var opt in f.Options)
                                list.AddOption(opt);

                            // Pre‑select if a matching value is supplied (single‑select list box)
                            if (!string.IsNullOrEmpty(f.Value))
                            {
                                int idx = Array.FindIndex(f.Options, o => o == f.Value);
                                if (idx >= 0)
                                    list.SelectedItems = new[] { idx + 1 }; // 1‑based array
                            }
                        }
                        else if (!string.IsNullOrEmpty(f.Value))
                        {
                            // No explicit options – add the value as the only option and select it
                            list.AddOption(f.Value);
                            list.SelectedItems = new[] { 1 };
                        }
                        doc.Form.Add(list, f.Page);
                        break;

                    case "signature":
                        var sig = new SignatureField(doc.Pages[f.Page], rect)
                        {
                            PartialName = f.Name
                        };
                        doc.Form.Add(sig, f.Page);
                        break;

                    default:
                        Console.Error.WriteLine($"Unsupported field type '{f.Type}' for field '{f.Name}'. Skipping.");
                        break;
                }
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }
}
