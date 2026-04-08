using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Aspose.Pdf;

class BatchFormExport
{
    static void Main()
    {
        // Input PDF files (adjust paths as needed)
        string[] pdfFiles = {
            "form1.pdf",
            "form2.pdf",
            "form3.pdf"
        };

        // Output JSON file that will contain a single JSON array
        const string outputJsonPath = "combined_forms.json";

        // Prepare a JSON array to hold each PDF's form data
        JsonArray combinedArray = new JsonArray();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Export form fields to a memory stream in JSON format
                using (MemoryStream ms = new MemoryStream())
                {
                    // ExportToJson writes a JSON array of field objects
                    doc.Form.ExportToJson(ms);
                    ms.Position = 0; // Reset stream for reading

                    // Read the JSON text
                    using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        string jsonText = reader.ReadToEnd();

                        // Parse the JSON text into a JsonNode
                        // The exported JSON is an array, so we parse it as JsonArray
                        JsonNode? node = JsonNode.Parse(jsonText);
                        if (node is JsonArray fieldArray)
                        {
                            // Add the entire array as an element of the combined array
                            // This results in a nested array per PDF; flatten if desired
                            combinedArray.Add(fieldArray);
                        }
                        else
                        {
                            Console.Error.WriteLine($"Unexpected JSON format in {pdfPath}");
                        }
                    }
                }
            }
        }

        // Write the combined JSON array to the output file
        // The result will be an array of arrays, each representing one PDF's form data
        File.WriteAllText(outputJsonPath, combinedArray.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

        Console.WriteLine($"Combined form data saved to '{outputJsonPath}'.");
    }
}