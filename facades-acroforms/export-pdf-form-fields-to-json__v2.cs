using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDirectory = "JsonParts";
        const int maxFieldsPerFile = 100;

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // If the input PDF does not exist, create a simple sample PDF with a few form fields
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdfWithFormFields(inputPdf);
            Console.WriteLine($"Sample PDF created: {inputPdf}");
        }

        // Load the PDF document
        Document pdfDocument;
        try
        {
            pdfDocument = new Document(inputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load PDF '{inputPdf}': {ex.Message}");
            return;
        }

        // Extract form fields into a dictionary
        var fieldDictionary = new Dictionary<string, object>();
        if (pdfDocument.Form != null && pdfDocument.Form.Fields != null)
        {
            foreach (Field field in pdfDocument.Form.Fields)
            {
                // Use the field's fully qualified name as the key (fallback to a safe placeholder)
                string fieldName = field.FullName ?? field.Name ?? "UnnamedField";
                // Get the field value as a string (handles different field types)
                string fieldValue = field.Value?.ToString() ?? string.Empty;
                fieldDictionary[fieldName] = fieldValue;
            }
        }
        else
        {
            Console.Error.WriteLine("No form fields found in the PDF.");
            return;
        }

        // Serialize the whole dictionary to JSON (indented for readability)
        string fullJson = JsonSerializer.Serialize(fieldDictionary, new JsonSerializerOptions { WriteIndented = true });

        // Parse the JSON back to a JsonDocument so we can enumerate properties safely
        using JsonDocument doc = JsonDocument.Parse(fullJson);
        JsonElement root = doc.RootElement;
        if (root.ValueKind != JsonValueKind.Object)
        {
            Console.Error.WriteLine("Exported JSON is not an object.");
            return;
        }

        List<JsonProperty> allProperties = root.EnumerateObject().ToList();
        int fileCounter = 1;

        for (int i = 0; i < allProperties.Count; i += maxFieldsPerFile)
        {
            IEnumerable<JsonProperty> batch = allProperties.Skip(i).Take(maxFieldsPerFile);
            string outPath = Path.Combine(outputDirectory, $"form_part_{fileCounter}.json");

            using FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write);
            using Utf8JsonWriter writer = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true });
            writer.WriteStartObject();
            foreach (JsonProperty prop in batch)
            {
                writer.WritePropertyName(prop.Name);
                prop.Value.WriteTo(writer);
            }
            writer.WriteEndObject();

            Console.WriteLine($"Created: {outPath}");
            fileCounter++;
        }
    }

    /// <summary>
    /// Creates a minimal PDF containing a few form fields. This makes the example self‑contained
    /// and removes the dependency on an external "input.pdf" file.
    /// </summary>
    private static void CreateSamplePdfWithFormFields(string path)
    {
        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Add a text box field
        TextBoxField txt = new TextBoxField(page, new Rectangle(100, 700, 300, 730))
        {
            PartialName = "SampleTextBox",
            Value = "Hello World"
        };
        doc.Form.Add(txt, 1);

        // Add a checkbox field (correct class name is CheckboxField)
        CheckboxField chk = new CheckboxField(page, new Rectangle(100, 650, 120, 670))
        {
            PartialName = "SampleCheckBox",
            Checked = true
        };
        doc.Form.Add(chk, 1);

        // Add a radio button group with two options – proper construction to avoid null field names
        RadioButtonField radioGroup = new RadioButtonField(page)
        {
            PartialName = "SampleRadioGroup"
        };

        RadioButtonOptionField radioOption1 = new RadioButtonOptionField(page, new Rectangle(100, 600, 120, 620))
        {
            OptionName = "Option1",
            Value = "Option1"
        };
        RadioButtonOptionField radioOption2 = new RadioButtonOptionField(page, new Rectangle(150, 600, 170, 620))
        {
            OptionName = "Option2",
            Value = "Option2"
        };

        // Add options to the group first, then add the group to the document
        radioGroup.Add(radioOption1);
        radioGroup.Add(radioOption2);
        doc.Form.Add(radioGroup, 1);

        // Save the PDF to the specified path
        doc.Save(path);
    }
}
