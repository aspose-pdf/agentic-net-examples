using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files containing form data
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file that will contain an array of form data objects
        const string outputJsonPath = "combined_forms.json";

        // Collect JSON objects exported from each PDF
        List<string> jsonObjects = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (using the standard Document constructor)
            using (Document doc = new Document(pdfPath))
            {
                // Export form fields to a memory stream in JSON format
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Form.ExportToJson(ms);
                    ms.Position = 0; // Reset stream position for reading

                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string json = reader.ReadToEnd().Trim();
                        // Ensure each exported JSON fragment is a valid JSON object
                        if (!json.StartsWith("{")) json = $"{{{json}}}";
                        jsonObjects.Add(json);
                    }
                }
            }
        }

        // Combine individual JSON objects into a single JSON array
        string combinedJson = "[" + string.Join(",", jsonObjects) + "]";

        // Write the combined JSON array to the output file
        File.WriteAllText(outputJsonPath, combinedJson);
        Console.WriteLine($"Combined form data saved to '{outputJsonPath}'.");
    }
}