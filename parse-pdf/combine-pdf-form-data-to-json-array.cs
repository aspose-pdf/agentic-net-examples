using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files containing form data
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file that will contain an array of all form data objects
        const string outputJsonPath = "combined_forms.json";

        // List to hold each PDF's exported JSON string
        List<string> jsonObjects = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (lifecycle rule: load)
            using (Document doc = new Document(pdfPath))
            {
                // Export form fields to JSON using a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Export form fields (lifecycle rule: use ExportToJson)
                    doc.Form.ExportToJson(ms);
                    // Convert the stream content to a UTF‑8 string
                    string json = Encoding.UTF8.GetString(ms.ToArray());
                    // Trim whitespace to ensure clean concatenation
                    json = json.Trim();
                    // Add the JSON object string to the collection
                    jsonObjects.Add(json);
                }
            }
        }

        // Combine all JSON objects into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonObjects) + "]";

        // Write the combined JSON array to the output file
        File.WriteAllText(outputJsonPath, combinedJson, Encoding.UTF8);

        Console.WriteLine($"Combined form data saved to '{outputJsonPath}'.");
    }
}