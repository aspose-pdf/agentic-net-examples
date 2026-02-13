using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF and JSON file paths (adjust as needed)
        const string pdfPath = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Error: JSON file not found at '{jsonPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Parse the JSON file into a dictionary of string -> string
            using FileStream jsonStream = File.OpenRead(jsonPath);
            using JsonDocument jsonDoc = JsonDocument.Parse(jsonStream);
            JsonElement root = jsonDoc.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
            {
                Console.Error.WriteLine("Error: JSON root must be an object with field/value pairs.");
                return;
            }

            // Iterate over each property in the JSON object
            foreach (JsonProperty prop in root.EnumerateObject())
            {
                string fieldName = prop.Name;
                string fieldValue = prop.Value.GetString() ?? string.Empty;

                // The Form collection returns a WidgetAnnotation. Use runtime type checks to handle supported field types.
                var widget = pdfDocument.Form[fieldName];
                if (widget == null)
                {
                    Console.WriteLine($"Warning: Field '{fieldName}' not found in the PDF form.");
                    continue;
                }

                // TextBoxField is available in all supported Aspose.Pdf versions.
                if (widget is TextBoxField textBox)
                {
                    textBox.Value = fieldValue;
                }
                // For CheckBoxField and RadioButtonOptionField we avoid compile‑time type references.
                // Instead we inspect the runtime type name and use dynamic to set the "Checked" property.
                else if (widget.GetType().Name == "CheckBoxField")
                {
                    dynamic checkBox = widget;
                    // Accept "true"/"false" (case‑insensitive) for check boxes.
                    checkBox.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                else if (widget.GetType().Name == "RadioButtonOptionField")
                {
                    dynamic radioButton = widget;
                    // For radio buttons treat "true" as the selected option.
                    radioButton.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    Console.WriteLine($"Info: Field '{fieldName}' is of type '{widget.GetType().Name}' which is not directly supported for value assignment.");
                }
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
