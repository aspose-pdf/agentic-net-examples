using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string schemaPath = "schema.json";
        const string outputPath = "generated_form.pdf";

        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"Schema file not found: {schemaPath}");
            return;
        }

        // Load and parse the JSON schema
        string json = File.ReadAllText(schemaPath);
        using JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;

        // Create a new PDF document
        using (Document pdf = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = pdf.Pages.Add();

            // Simple layout variables
            double marginLeft = 50;
            double marginTop = 750;
            double fieldWidth = 300;
            double fieldHeight = 20;
            double verticalSpacing = 30;

            // Ensure the schema has a "properties" object
            if (root.TryGetProperty("properties", out JsonElement properties))
            {
                int fieldIndex = 0;
                foreach (JsonProperty prop in properties.EnumerateObject())
                {
                    string fieldName = prop.Name;
                    JsonElement propDef = prop.Value;
                    string fieldType = propDef.GetProperty("type").GetString();

                    // Calculate field rectangle
                    double llx = marginLeft;
                    double lly = marginTop - fieldIndex * verticalSpacing - fieldHeight;
                    double urx = llx + fieldWidth;
                    double ury = lly + fieldHeight;
                    Rectangle rect = new Rectangle(llx, lly, urx, ury);

                    // Create appropriate AcroForm field
                    if (fieldType == "string")
                    {
                        TextBoxField txtField = new TextBoxField(page, rect)
                        {
                            PartialName = fieldName,
                            Value = "" // default empty
                        };
                        pdf.Form.Add(txtField);
                    }
                    else if (fieldType == "boolean")
                    {
                        CheckboxField chkField = new CheckboxField(page, rect)
                        {
                            PartialName = fieldName,
                            // "Off" means unchecked; "Yes" (or any non‑Off value) would be checked
                            Value = "Off"
                        };
                        pdf.Form.Add(chkField);
                    }
                    else // fallback to a text box for numbers, integers, etc.
                    {
                        TextBoxField txtField = new TextBoxField(page, rect)
                        {
                            PartialName = fieldName,
                            Value = ""
                        };
                        pdf.Form.Add(txtField);
                    }

                    // Add a label for the field (simple text fragment)
                    TextFragment label = new TextFragment(fieldName + ":")
                    {
                        Position = new Position(marginLeft - 40, lly + 5)
                    };
                    label.TextState.FontSize = 12;
                    label.TextState.ForegroundColor = Color.Black;
                    page.Paragraphs.Add(label);

                    fieldIndex++;
                }
            }
            else
            {
                Console.Error.WriteLine("The JSON schema does not contain a 'properties' object.");
                return;
            }

            // Save the PDF (no SaveOptions needed for PDF output)
            pdf.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
