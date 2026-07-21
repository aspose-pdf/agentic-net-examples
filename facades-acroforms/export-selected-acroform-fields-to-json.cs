using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will contain only the selected fields
        const string outputJsonPath = "selected_fields.json";

        // List of fully‑qualified field names that should be exported
        string[] fieldsToExport = new[]
        {
            "Form1.TextBox1",
            "Form1.CheckBox1",
            "Form1.ComboBox1"
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the Facades Form object on the PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Export all form fields to a memory stream as JSON
                using (MemoryStream allJsonStream = new MemoryStream())
                {
                    // ExportJson writes the JSON representation of all fields
                    form.ExportJson(allJsonStream, indented: true);

                    // Read the JSON text from the memory stream
                    allJsonStream.Position = 0;
                    using (StreamReader reader = new StreamReader(allJsonStream))
                    {
                        string allJson = reader.ReadToEnd();

                        // Parse the JSON; Aspose exports an array of objects,
                        // each object contains at least a "FullName" property.
                        JsonDocument doc = JsonDocument.Parse(allJson);
                        JsonElement root = doc.RootElement;

                        // Prepare a list to hold the filtered field objects
                        var filteredFields = new List<Dictionary<string, JsonElement>>();

                        foreach (JsonElement fieldElement in root.EnumerateArray())
                        {
                            if (fieldElement.TryGetProperty("FullName", out JsonElement nameProp))
                            {
                                string fullName = nameProp.GetString();
                                if (fieldsToExport.Contains(fullName))
                                {
                                    // Copy all properties of the matching field into a dictionary
                                    var fieldDict = new Dictionary<string, JsonElement>();
                                    foreach (JsonProperty prop in fieldElement.EnumerateObject())
                                    {
                                        // Clone the JsonElement to avoid disposal issues
                                        fieldDict[prop.Name] = prop.Value.Clone();
                                    }
                                    filteredFields.Add(fieldDict);
                                }
                            }
                        }

                        // Serialize the filtered collection back to JSON
                        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                        string filteredJson = JsonSerializer.Serialize(filteredFields, jsonOptions);

                        // Write the filtered JSON to the output file
                        File.WriteAllText(outputJsonPath, filteredJson);
                    }
                }
            }

            Console.WriteLine($"Selected fields exported to '{outputJsonPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}