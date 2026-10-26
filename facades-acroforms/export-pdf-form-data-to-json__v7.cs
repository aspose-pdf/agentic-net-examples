using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Newtonsoft.Json;

// Define a typed class that matches the PDF form field names.
// Properties are nullable to satisfy the non‑nullable warnings when the JSON does not contain a value.
public class MyFormData
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    // Add additional properties as needed, matching the form field names.
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";   // source PDF with AcroForm fields
        const string jsonPath = "form_data.json"; // temporary JSON export file

        // Ensure the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Export form data from the PDF to a JSON file using the Document API.
        // -----------------------------------------------------------------
        Document doc = new Document(pdfPath);

        // Collect field name/value pairs.
        var fieldValues = new Dictionary<string, string>();
        foreach (Field field in doc.Form)
        {
            // Some field types expose the value via the "Value" property; others use "PartialName".
            // We store the string representation (empty string if null).
            fieldValues[field.PartialName] = field.Value?.ToString() ?? string.Empty;
        }

        // Serialize the dictionary to JSON and write it to a file.
        string jsonExport = JsonConvert.SerializeObject(fieldValues, Formatting.Indented);
        File.WriteAllText(jsonPath, jsonExport);

        // -----------------------------------------------------------------
        // Read the generated JSON and deserialize it into a strongly‑typed object.
        // -----------------------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON export failed: {jsonPath}");
            return;
        }

        string jsonContent = File.ReadAllText(jsonPath);
        // Direct deserialization into MyFormData works because the JSON keys match property names.
        MyFormData? data = JsonConvert.DeserializeObject<MyFormData>(jsonContent);

        // -----------------------------------------------------------------
        // Further processing of the typed object.
        // -----------------------------------------------------------------
        if (data != null)
        {
            Console.WriteLine($"Name : {data.Name}");
            Console.WriteLine($"Email: {data.Email}");
            Console.WriteLine($"Phone: {data.Phone}");

            // Example: simple validation.
            if (string.IsNullOrWhiteSpace(data.Email))
                Console.WriteLine("Warning: Email field is empty.");
        }
        else
        {
            Console.Error.WriteLine("Deserialization returned null.");
        }

        // Optional: clean up the temporary JSON file.
        try { File.Delete(jsonPath); } catch { /* ignore cleanup errors */ }
    }
}
