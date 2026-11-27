using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "form_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Ensure the Document is disposed properly
            using (Document doc = new Document(inputPath))
            {
                var fieldList = new List<object>();

                // Iterate over all form fields in the PDF
                foreach (Field field in doc.Form)
                {
                    string name = field.FullName;
                    string type = field.GetType().Name;
                    string value = field.Value?.ToString() ?? string.Empty;

                    fieldList.Add(new { Name = name, Type = type, Value = value });
                }

                // Serialize to JSON with indentation
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(fieldList, jsonOptions);

                // Write JSON to file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Form fields exported to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}