using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;               // Aspose.Pdf.Facades.Form
using Newtonsoft.Json;                  // Json.NET

// Define a typed class that matches the expected JSON structure.
// For generic form data we can store all fields in a dictionary.
public class FormData
{
    // The JSON exported by Aspose.Pdf.Facades.Form is a simple
    // key‑value map where the key is the full field name and the
    // value is the field's string representation.
    public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input_form.pdf";

        // Ensure the input file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Export the form data to a JSON string using Aspose.Pdf.Facades.Form.
        string json;
        using (var form = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            // ExportJson writes the JSON to a stream.
            using (var ms = new MemoryStream())
            {
                // The second parameter (true) requests indented output for readability.
                form.ExportJson(ms, true);
                ms.Position = 0; // Reset stream before reading.
                using (var reader = new StreamReader(ms))
                {
                    json = reader.ReadToEnd();
                }
            }
        }

        Console.WriteLine("Exported JSON:");
        Console.WriteLine(json);
        Console.WriteLine();

        // Deserialize the JSON into the typed FormData object using Json.NET.
        // The exported JSON has the shape: { "FieldName1": "Value1", "FieldName2": "Value2", ... }
        // We map it to the Fields dictionary.
        var fieldsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        var formData = new FormData { Fields = fieldsDictionary ?? new Dictionary<string, string>() };

        // Example of further processing: iterate over the fields.
        Console.WriteLine("Deserialized form data:");
        foreach (var kvp in formData.Fields)
        {
            Console.WriteLine($"{kvp.Key} = {kvp.Value}");
        }

        // (Optional) If you need to modify the data and import it back:
        // formData.Fields["FirstName"] = "John";
        // string modifiedJson = JsonConvert.SerializeObject(formData.Fields, Formatting.Indented);
        // using (var importForm = new Aspose.Pdf.Facades.Form(inputPdfPath, "output_form.pdf"))
        // using (var importStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(modifiedJson)))
        // {
        //     importForm.ImportJson(importStream);
        //     importForm.Save(); // Saves to the output file specified in the constructor.
        // }
    }
}