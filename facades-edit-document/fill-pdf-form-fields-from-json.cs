using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "template_form.pdf";
        const string outputPdfPath  = "filled_form.pdf";
        const string configFilePath = "default_values.json";

        // Verify files exist
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

        // Load default values from a simple JSON file:
        // { "FirstName": "John", "LastName": "Doe", "Country": "USA" }
        Dictionary<string, string> defaultValues;
        try
        {
            string json = File.ReadAllText(configFilePath);
            defaultValues = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // Open the PDF form using Aspose.Pdf.Facades.Form
        using (Form form = new Form(inputPdfPath))
        {
            // Iterate over each entry in the configuration and fill the matching field
            foreach (var kvp in defaultValues)
            {
                string fieldName  = kvp.Key;
                string fieldValue = kvp.Value;

                // Ensure the field exists in the document before attempting to fill it
                if (Array.Exists(form.FieldNames, name => name.Equals(fieldName, StringComparison.Ordinal)))
                {
                    bool filled = form.FillField(fieldName, fieldValue);
                    if (!filled)
                    {
                        Console.Error.WriteLine($"Failed to fill field: {fieldName}");
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Field not found in PDF: {fieldName}");
                }
            }

            // Save the updated PDF to the output path
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}