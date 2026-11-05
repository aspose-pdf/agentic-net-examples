using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string jsonSchemaPath = "schema.json";
        const string outputPdfPath = "generated_form.pdf";

        if (!File.Exists(jsonSchemaPath))
        {
            Console.Error.WriteLine($"JSON schema file not found: {jsonSchemaPath}");
            return;
        }

        // Load and parse the JSON schema
        string jsonContent = File.ReadAllText(jsonSchemaPath);
        using JsonDocument schemaDoc = JsonDocument.Parse(jsonContent);
        JsonElement root = schemaDoc.RootElement;

        // Expecting a "properties" object under the root
        if (!root.TryGetProperty("properties", out JsonElement properties))
        {
            Console.Error.WriteLine("Invalid JSON schema: missing 'properties' object.");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to host the form fields
            Page page = pdfDoc.Pages.Add();

            // Simple layout parameters
            const double left = 50;
            const double width = 300;
            const double height = 20;
            double top = 800; // start from top of the page
            const double verticalSpacing = 30;

            foreach (JsonProperty prop in properties.EnumerateObject())
            {
                string fieldName = prop.Name;
                string fieldType = "string"; // default type

                if (prop.Value.TryGetProperty("type", out JsonElement typeElement))
                {
                    fieldType = typeElement.GetString()?.ToLowerInvariant() ?? "string";
                }

                // Define the rectangle for the field
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(left, top - height, left + width, top);

                // Create appropriate form field based on JSON schema type
                switch (fieldType)
                {
                    case "boolean":
                        // CheckBox field
                        CheckboxField checkBox = new CheckboxField(pdfDoc, rect)
                        {
                            PartialName = fieldName,
                            Value = "Off" // default unchecked
                        };
                        pdfDoc.Form.Add(checkBox);
                        break;

                    case "integer":
                    case "number":
                        // Numeric input as TextBox (Aspose.Pdf does not have a dedicated numeric field)
                        TextBoxField numericBox = new TextBoxField(pdfDoc, rect)
                        {
                            PartialName = fieldName,
                            Value = string.Empty
                        };
                        pdfDoc.Form.Add(numericBox);
                        break;

                    case "string":
                    default:
                        // General text input
                        TextBoxField textBox = new TextBoxField(pdfDoc, rect)
                        {
                            PartialName = fieldName,
                            Value = string.Empty
                        };
                        pdfDoc.Form.Add(textBox);
                        break;
                }

                // Move down for the next field
                top -= verticalSpacing;
            }

            // Save the generated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with AcroForm fields generated at '{outputPdfPath}'.");
    }
}