using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class FormDataTransformer
{
    // Adjust these paths as needed
    const string sourcePdfPath = "source_form.pdf";
    const string targetPdfPath = "target_form.pdf";
    const string outputPdfPath = "target_filled.pdf";

    static void Main()
    {
        // Verify source PDF exists before attempting to load it
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Error: Source PDF file '{sourcePdfPath}' not found.");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Error: Target PDF file '{targetPdfPath}' not found.");
            return;
        }

        try
        {
            // 1. Load the PDF that contains the original form data
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // 2. Export form fields to JSON (in‑memory)
                using (MemoryStream exportStream = new MemoryStream())
                {
                    sourceDoc.Form.ExportToJson(exportStream);
                    exportStream.Position = 0; // rewind

                    // 3. Read JSON text
                    string jsonText = new StreamReader(exportStream, Encoding.UTF8).ReadToEnd();

                    // 4. Deserialize to a dictionary for manipulation (handle possible null)
                    var originalData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonText) ?? new Dictionary<string, object>();

                    // 5. Transform the data to a new schema
                    //    Example: rename fields and uppercase string values
                    var transformedData = new Dictionary<string, object>();
                    foreach (var kvp in originalData)
                    {
                        // Example mapping: "OldFieldName" -> "NewFieldName"
                        string newKey = kvp.Key switch
                        {
                            "FirstName" => "GivenName",
                            "LastName"  => "FamilyName",
                            _           => kvp.Key // keep unchanged if no mapping
                        };

                        // Example value transformation: uppercase strings
                        object newValue = kvp.Value;
                        if (kvp.Value is string s)
                            newValue = s.ToUpperInvariant();

                        transformedData[newKey] = newValue;
                    }

                    // 6. Serialize transformed data back to JSON
                    string transformedJson = JsonSerializer.Serialize(transformedData, new JsonSerializerOptions { WriteIndented = true });

                    // 7. Load the target PDF where data will be imported
                    using (Document targetDoc = new Document(targetPdfPath))
                    {
                        // 8. Import transformed JSON into the target form
                        using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(transformedJson)))
                        {
                            targetDoc.Form.ImportFromJson(importStream);
                        }

                        // 9. Save the updated PDF
                        targetDoc.Save(outputPdfPath);
                    }
                }
            }

            Console.WriteLine($"Form data transferred and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
