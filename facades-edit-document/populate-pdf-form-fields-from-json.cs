using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "template.pdf";      // PDF with form fields
        const string outputPdfPath  = "filled.pdf";        // Resulting PDF
        const string configFilePath = "defaultValues.json"; // {"FirstName":"John","LastName":"Doe"}

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

        try
        {
            // Load default values from configuration (JSON key/value pairs)
            Dictionary<string, string> defaultValues;
            using (FileStream cfgStream = File.OpenRead(configFilePath))
            {
                defaultValues = JsonSerializer.Deserialize<Dictionary<string, string>>(cfgStream);
            }

            // Initialize Form facade with the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Fill each field with the corresponding default value
                foreach (KeyValuePair<string, string> kvp in defaultValues)
                {
                    // FillField returns true if the field exists and is filled successfully
                    bool filled = form.FillField(kvp.Key, kvp.Value);
                    if (!filled)
                    {
                        Console.WriteLine($"Warning: Field '{kvp.Key}' not found or could not be filled.");
                    }
                }

                // Save the updated PDF
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}