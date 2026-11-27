using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormFieldsToJson
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF containing form fields
        const string jsonPath = "formFields.json";    // output JSON file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(pdfPath))
        {
            // Prepare a dictionary to hold PartialName‑Value pairs
            var fieldValues = new Dictionary<string, string>();

            // Iterate over all form fields in the document using the correct Field type from Aspose.Pdf.Forms
            foreach (Field field in doc.Form.Fields)
            {
                // Some fields may have a null Value; treat it as empty string
                string value = field.Value?.ToString() ?? string.Empty;
                fieldValues[field.PartialName] = value;
            }

            // Serialize the dictionary to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fieldValues, jsonOptions);

            // Write the JSON string to the output file
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Form fields exported to '{jsonPath}'.");
    }
}