using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "template.pdf";      // PDF with form fields
        const string outputPdfPath  = "filled.pdf";        // Resulting PDF
        const string configFilePath = "defaultValues.json"; // JSON: { "FieldName": "Value", ... }

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Config file not found: {configFilePath}");
            return;
        }

        // Load default values from JSON configuration file
        Dictionary<string, string> defaultValues;
        try
        {
            string json = File.ReadAllText(configFilePath);
            defaultValues = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Open the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the Form facade with the opened document
            Form form = new Form(doc);

            // Fill each field with its default value from the configuration
            foreach (var kvp in defaultValues)
            {
                string fieldName  = kvp.Key;
                string fieldValue = kvp.Value;

                // Attempt to fill the field; ignore failures (e.g., field not found)
                bool filled = form.FillField(fieldName, fieldValue);
                if (!filled)
                {
                    Console.WriteLine($"Warning: Field \"{fieldName}\" not found or could not be filled.");
                }
            }

            // Save the updated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}