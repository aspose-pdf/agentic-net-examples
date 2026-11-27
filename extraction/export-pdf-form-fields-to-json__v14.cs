using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "FormG.pdf";
        const string outputPath = "FormG_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                var form = doc.Form;
                // If there are no form fields, write an empty JSON array
                if (form == null || form.Fields == null || form.Fields.Length == 0)
                {
                    File.WriteAllText(outputPath, "[]");
                    Console.WriteLine("No form fields found.");
                    return;
                }

                var fieldList = new List<Dictionary<string, string>>();

                // Enumerate each form field and capture PartialName and Value
                foreach (var field in form.Fields)
                {
                    string name = field.PartialName ?? string.Empty;
                    string value = field.Value?.ToString() ?? string.Empty;

                    var dict = new Dictionary<string, string>
                    {
                        ["PartialName"] = name,
                        ["Value"] = value
                    };
                    fieldList.Add(dict);
                }

                // Serialize the list to JSON with indentation
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(fieldList, jsonOptions);
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {fieldList.Count} fields to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
