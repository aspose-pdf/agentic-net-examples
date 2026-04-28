using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files containing form fields
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file that will contain merged form data
        const string outputJsonPath = "merged_form_data.json";

        // List to hold each document's form data as a dictionary
        var allFormData = new List<Dictionary<string, JsonElement>>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Use the Facades Form class to work with AcroForm data
            using (Form form = new Form(pdfPath))
            {
                // Export the form fields to a JSON stream (button fields are ignored)
                using (MemoryStream jsonStream = new MemoryStream())
                {
                    // The second parameter 'false' means do not export empty fields
                    form.ExportJson(jsonStream, false);
                    jsonStream.Position = 0;

                    // Parse the JSON into a dictionary of field name -> value
                    using (JsonDocument doc = JsonDocument.Parse(jsonStream))
                    {
                        var fieldDict = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase);
                        foreach (JsonProperty prop in doc.RootElement.EnumerateObject())
                        {
                            fieldDict[prop.Name] = prop.Value.Clone();
                        }
                        allFormData.Add(fieldDict);
                    }
                }
            }
        }

        // Serialize the list of dictionaries as a JSON array
        string mergedJson = JsonSerializer.Serialize(allFormData, new JsonSerializerOptions { WriteIndented = true });

        // Write the merged JSON to the output file
        File.WriteAllText(outputJsonPath, mergedJson);
        Console.WriteLine($"Merged form data saved to '{outputJsonPath}'.");
    }
}
