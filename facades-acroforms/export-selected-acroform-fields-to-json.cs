using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "selected_fields.json";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define the form field names that should be exported
        string[] fieldNames = new[] { "FirstName", "LastName", "Email" };
        var fieldSet = new HashSet<string>(fieldNames, StringComparer.OrdinalIgnoreCase);

        // Load the PDF document (core API)
        using var document = new Document(inputPdf);

        // Collect values of the selected fields
        var selectedData = new Dictionary<string, string>();
        if (document.Form != null && document.Form.Fields != null)
        {
            foreach (Field field in document.Form.Fields)
            {
                string name = field?.FullName ?? field?.Name;
                if (name != null && fieldSet.Contains(name))
                {
                    // Convert the field value to string safely
                    string value = field?.Value?.ToString() ?? string.Empty;
                    selectedData[name] = value;
                }
            }
        }

        // Serialize the dictionary to JSON (indented for readability)
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(selectedData, jsonOptions);
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Selected form fields have been exported to '{outputJson}'.");
    }
}
