using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePdfPath = "template.pdf";      // PDF with form fields
        const string jsonDataPath    = "data.json";        // JSON file: { "FirstName":"John", "LastName":"Doe", ... }
        const string outputPdfPath   = "filled_form.pdf";

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load JSON into a dictionary (key = field name, value = field value)
        Dictionary<string, string> fieldValues;
        using (FileStream jsonStream = File.OpenRead(jsonDataPath))
        {
            fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonStream);
        }

        if (fieldValues == null)
        {
            Console.Error.WriteLine("Failed to parse JSON data.");
            return;
        }

        // Open the PDF form using the Facades Form class
        using (Form form = new Form(templatePdfPath))
        {
            // Iterate over each key/value pair and fill the corresponding field
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // FillField expects the full field name as defined in the PDF
                bool filled = form.FillField(kvp.Key, kvp.Value);
                if (!filled)
                {
                    Console.WriteLine($"Warning: field \"{kvp.Key}\" not found or could not be filled.");
                }
            }

            // Save the filled PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form filled PDF saved to \"{outputPdfPath}\".");
    }
}