using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for form field classes

// Define a model that matches the JSON schema for form fields
public class FormFieldDefinition
{
    public string? Type { get; set; }          // e.g., "TextBox", "CheckBox", "RadioButton"
    public string? Name { get; set; }          // field name (partial name)
    public string? Value { get; set; }         // default value or checked state ("true"/"false")
    public double[]? Rect { get; set; }        // [llx, lly, urx, ury] in points
    public string[]? Options { get; set; }     // for RadioButton groups (optional)
}

class Program
{
    static void Main()
    {
        const string jsonPath   = "form_schema.json";   // input JSON describing the fields
        const string outputPath = "generated_form.pdf"; // output PDF with AcroForm fields

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON schema not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of field definitions
        List<FormFieldDefinition> fields;
        try
        {
            string json = File.ReadAllText(jsonPath);
            fields = JsonSerializer.Deserialize<List<FormFieldDefinition>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Create a new PDF document and add a single blank page
        using (Document doc = new Document())
        {
            // Add a page to host the form fields
            Page page = doc.Pages.Add();

            // Iterate over each field definition and create the corresponding AcroForm field
            foreach (var def in fields)
            {
                // Validate rectangle coordinates
                if (def.Rect == null || def.Rect.Length != 4)
                {
                    Console.Error.WriteLine($"Invalid rectangle for field '{def.Name}'. Skipping.");
                    continue;
                }

                // Use fully qualified Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    def.Rect[0], // llx
                    def.Rect[1], // lly
                    def.Rect[2], // urx
                    def.Rect[3]  // ury
                );

                // Create field based on its type
                switch (def.Type?.Trim().ToLowerInvariant())
                {
                    case "textbox":
                    case "text":
                        TextBoxField txtField = new TextBoxField(page, rect)
                        {
                            PartialName = def.Name ?? string.Empty,
                            Value = def.Value ?? string.Empty
                        };
                        doc.Form.Add(txtField);
                        break;

                    case "checkbox":
                        CheckboxField chkField = new CheckboxField(page, rect)
                        {
                            PartialName = def.Name ?? string.Empty
                        };
                        // Set Checked via reflection to stay compatible with all Aspose.Pdf versions
                        SetCheckedProperty(chkField, def.Value);
                        doc.Form.Add(chkField);
                        break;

                    case "radiobutton":
                    case "radio":
                        if (def.Options == null || def.Options.Length == 0)
                        {
                            Console.Error.WriteLine($"RadioButton field '{def.Name}' missing options. Skipping.");
                            continue;
                        }

                        double offsetY = 0;
                        foreach (var opt in def.Options)
                        {
                            // Adjust rectangle for each option (simple vertical stacking)
                            Aspose.Pdf.Rectangle optRect = new Aspose.Pdf.Rectangle(
                                rect.LLX,
                                rect.LLY - offsetY,
                                rect.URX,
                                rect.URY - offsetY
                            );

                            RadioButtonOptionField radioOption = new RadioButtonOptionField(page, optRect)
                            {
                                PartialName = def.Name ?? string.Empty,
                                Value = opt // export value for this option
                            };
                            // Set Checked via reflection – property may not exist in some versions
                            SetCheckedProperty(radioOption, def.Value, opt);
                            doc.Form.Add(radioOption);
                            offsetY += 20; // arbitrary spacing between radio options
                        }
                        break;

                    default:
                        Console.Error.WriteLine($"Unsupported field type '{def.Type}' for field '{def.Name}'. Skipping.");
                        break;
                }
            }

            // Save the generated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }

    /// <summary>
    /// Sets the "Checked" property on a form field using reflection.
    /// Works for both CheckboxField and RadioButtonOptionField where the property may be missing in some Aspose.Pdf versions.
    /// </summary>
    /// <param name="field">The form field instance.</param>
    /// <param name="fieldValue">The value from JSON (e.g., "true" or the default radio option).</param>
    /// <param name="optionValue">Optional – the specific option value for a radio button. If supplied, the field is checked only when the two match.</param>
    private static void SetCheckedProperty(object field, string? fieldValue, string? optionValue = null)
    {
        if (string.IsNullOrEmpty(fieldValue))
            return;

        // Determine whether the field should be marked as checked
        bool shouldCheck;
        if (optionValue != null)
        {
            // Radio button: check only if the default value equals the option value
            shouldCheck = string.Equals(fieldValue, optionValue, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            // Checkbox: any "true" (case‑insensitive) means checked
            shouldCheck = string.Equals(fieldValue, "true", StringComparison.OrdinalIgnoreCase);
        }

        if (!shouldCheck)
            return;

        // Use reflection to find a writable "Checked" property
        PropertyInfo? prop = field.GetType().GetProperty("Checked", BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            try
            {
                prop.SetValue(field, true);
            }
            catch (Exception ex)
            {
                // If the property exists but cannot be set for some reason, log and continue.
                Console.Error.WriteLine($"Failed to set Checked on {field.GetType().Name}: {ex.Message}");
            }
        }
        else
        {
            // Property not present – some older Aspose.Pdf versions expose a different API.
            // No action needed; the field will remain unchecked.
        }
    }
}
