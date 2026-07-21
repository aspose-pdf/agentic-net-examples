using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Forms;               // Form handling (ExportToJson)

class BatchFormDataExtractor
{
    static void Main()
    {
        // Input PDF files (adjust paths as needed)
        string[] pdfFiles = {
            "Form1.pdf",
            "Form2.pdf",
            "Form3.pdf"
        };

        // Output JSON file that will contain an array of form data objects
        const string outputJsonPath = "CombinedFormData.json";

        // List to hold each PDF's form data as a JsonElement
        var formDataElements = new List<JsonElement>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Export form fields to a memory stream in JSON format
                using (MemoryStream ms = new MemoryStream())
                {
                    // ExportToJson writes JSON directly to the provided stream
                    doc.Form.ExportToJson(ms);

                    // Reset stream position to read the JSON text
                    ms.Position = 0;
                    using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        string jsonText = reader.ReadToEnd();

                        // Parse the JSON text into a JsonDocument to obtain a JsonElement
                        using (JsonDocument jsonDoc = JsonDocument.Parse(jsonText))
                        {
                            // The exported JSON represents an object; add it to the list
                            formDataElements.Add(jsonDoc.RootElement.Clone());
                        }
                    }
                }
            }
        }

        // Combine all individual form objects into a JSON array
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string combinedJson = JsonSerializer.Serialize(formDataElements, options);

        // Write the combined JSON array to the output file
        File.WriteAllText(outputJsonPath, combinedJson, Encoding.UTF8);

        Console.WriteLine($"Combined form data saved to '{outputJsonPath}'.");
    }
}