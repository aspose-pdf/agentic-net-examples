using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "template.pdf";      // PDF with form fields
        const string outputPdfPath  = "filled.pdf";        // Resulting PDF
        const string configFilePath = "formDefaults.json"; // JSON: { "FieldName":"Value", ... }

        // Load default values from configuration file
        Dictionary<string, string> fieldDefaults = LoadFieldDefaults(configFilePath);
        if (fieldDefaults == null || fieldDefaults.Count == 0)
        {
            Console.Error.WriteLine("No field defaults found.");
            return;
        }

        // Open the PDF form using Aspose.Pdf.Facades.Form
        using (Form form = new Form(inputPdfPath))
        {
            // Fill each field with its default value
            foreach (var kvp in fieldDefaults)
            {
                // Form.FillField expects the fully qualified field name
                bool filled = form.FillField(kvp.Key, kvp.Value);
                if (!filled)
                {
                    Console.Error.WriteLine($"Field '{kvp.Key}' not found or could not be filled.");
                }
            }

            // Save the updated PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }

    // Reads a JSON file containing field name/value pairs into a dictionary
    private static Dictionary<string, string> LoadFieldDefaults(string path)
    {
        if (!File.Exists(path))
        {
            Console.Error.WriteLine($"Configuration file not found: {path}");
            return null;
        }

        try
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return null;
        }
    }
}