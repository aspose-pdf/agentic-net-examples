using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms; // needed for form field classes

// Define a class that matches the JSON structure for a form field
public class FieldDefinition
{
    public string? Name { get; set; }          // field name (nullable to satisfy non‑nullable warnings)
    public string? Type { get; set; }          // e.g., "TextBox", "CheckBox", "RadioButton"
    public int Page { get; set; }              // 1‑based page index
    public RectangleDto? Rect { get; set; }    // rectangle coordinates (nullable)
    public JsonElement Value { get; set; }     // value can be string, bool, number, etc.
}

// Helper DTO for rectangle coordinates (matches JSON)
public class RectangleDto
{
    public double LLX { get; set; }
    public double LLY { get; set; }
    public double URX { get; set; }
    public double URY { get; set; }
}

class Program
{
    static void Main()
    {
        const string jsonPath   = "fields.json";      // input JSON file
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
            string jsonContent = File.ReadAllText(jsonPath);
            fields = JsonSerializer.Deserialize<List<FieldDefinition>>(jsonContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<FieldDefinition>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Create a new PDF document with a single blank page (more pages can be added later)
        using (Document pdfDoc = new Document())
        {
            // Ensure at least one page exists; Aspose.Pdf creates a default page when saved,
            // but we add it explicitly to have a known page count for field placement.
            pdfDoc.Pages.Add();

            // Iterate over each field definition and create the appropriate form field
            foreach (var field in fields)
            {
                // Basic validation
                if (field.Name == null)
                {
                    Console.Error.WriteLine("Field definition missing 'Name'. Skipping.");
                    continue;
                }
                if (field.Rect == null)
                {
                    Console.Error.WriteLine($"Field '{field.Name}' missing rectangle definition. Skipping.");
                    continue;
                }

                // Convert the rectangle DTO to Aspose.Pdf.Rectangle (1‑based page indexing)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    field.Rect.LLX,
                    field.Rect.LLY,
                    field.Rect.URX,
                    field.Rect.URY);

                // Guard against invalid page numbers
                if (field.Page < 1 || field.Page > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {field.Page} for field '{field.Name}'. Skipping.");
                    continue;
                }

                // Create field based on its type
                switch (field.Type?.Trim().ToLowerInvariant())
                {
                    case "textbox":
                        {
                            string txtValue = field.Value.ValueKind == JsonValueKind.String
                                ? field.Value.GetString()!
                                : string.Empty;
                            var txtField = new TextBoxField(pdfDoc.Pages[field.Page], rect)
                            {
                                PartialName = field.Name,
                                Value = txtValue
                            };
                            pdfDoc.Form.Add(txtField, field.Page);
                        }
                        break;

                    case "checkbox":
                        {
                            bool chkValue = field.Value.ValueKind == JsonValueKind.True;
                            var chkField = new CheckboxField(pdfDoc.Pages[field.Page], rect)
                            {
                                PartialName = field.Name,
                                // Aspose expects "On" for checked, empty string for unchecked
                                Value = chkValue ? "On" : string.Empty
                            };
                            pdfDoc.Form.Add(chkField, field.Page);
                        }
                        break;

                    case "radiobutton":
                        {
                            // Use RadioButtonOptionField for a single option
                            string exportValue = field.Value.ValueKind == JsonValueKind.String
                                ? field.Value.GetString()!
                                : "On";
                            var radioField = new RadioButtonOptionField(pdfDoc.Pages[field.Page], rect)
                            {
                                PartialName = field.Name,
                                Value = exportValue
                            };
                            pdfDoc.Form.Add(radioField, field.Page);
                        }
                        break;

                    case "listbox":
                        {
                            var listBox = new ListBoxField(pdfDoc.Pages[field.Page], rect)
                            {
                                PartialName = field.Name
                            };
                            List<string> items = new List<string>();
                            if (field.Value.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var item in field.Value.EnumerateArray())
                                {
                                    if (item.ValueKind == JsonValueKind.String)
                                        items.Add(item.GetString()!);
                                }
                            }
                            // Add options to the list box
                            foreach (var itm in items)
                                listBox.AddOption(itm);
                            // Select the first item if any
                            if (items.Count > 0)
                                listBox.SelectedItems = new int[] { 0 };
                            pdfDoc.Form.Add(listBox, field.Page);
                        }
                        break;

                    case "combobox":
                        {
                            var comboBox = new ComboBoxField(pdfDoc.Pages[field.Page], rect)
                            {
                                PartialName = field.Name
                            };
                            List<string> items = new List<string>();
                            if (field.Value.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var item in field.Value.EnumerateArray())
                                {
                                    if (item.ValueKind == JsonValueKind.String)
                                        items.Add(item.GetString()!);
                                }
                            }
                            // Add options to the combo box
                            foreach (var itm in items)
                                comboBox.AddOption(itm);
                            // Pre‑select the first option if any (use Value, not SelectedItem)
                            if (items.Count > 0)
                                comboBox.Value = items[0];
                            pdfDoc.Form.Add(comboBox, field.Page);
                        }
                        break;

                    default:
                        Console.Error.WriteLine($"Unsupported field type '{field.Type}' for field '{field.Name}'. Skipping.");
                        break;
                }
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form fields created: {outputPath}");
    }
}
