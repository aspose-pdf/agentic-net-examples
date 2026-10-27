using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for Document.Form extensions

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "filled_form.pdf";

        // Verify that the source PDF and JSON files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // ------------------------------------------------------------
                // Import form field values from the JSON file.
                // Aspose.Pdf.Forms.Form does not expose an ImportDataFromJson
                // method, so we read the JSON ourselves and assign the values
                // to the corresponding fields via the Form.Fields collection.
                // ------------------------------------------------------------
                var jsonContent = File.ReadAllText(jsonPath);
                var fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

                if (fieldValues != null && doc.Form?.Fields != null)
                {
                    foreach (var kvp in fieldValues)
                    {
                        // Find the field with the matching name (case‑sensitive)
                        var field = doc.Form.Fields.FirstOrDefault(f => f.Name == kvp.Key);
                        if (field != null)
                        {
                            // Most field types expose a Value property that accepts an object.
                            // Converting the string to the appropriate type is left to the caller.
                            field.Value = kvp.Value;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Warning: No form field named '{kvp.Key}' was found in the PDF.");
                        }
                    }
                }
                else
                {
                    Console.Error.WriteLine("No form fields or JSON data to import.");
                }

                // Save the updated PDF to a new file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPath}'.");
        }
        catch (JsonException ex)
        {
            // Thrown when the JSON is malformed or cannot be deserialized into a dictionary
            Console.Error.WriteLine($"Invalid JSON data: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch‑all for any other errors (e.g., I/O, parsing)
            Console.Error.WriteLine($"Error processing PDF form: {ex.Message}");
        }
    }
}
