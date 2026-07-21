using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // for accessing form fields

class Program
{
    static void Main()
    {
        // Input PDF files containing form fields
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file that will contain aggregated form data
        const string outputJsonPath = "merged_form_data.json";

        // Dictionary to collect values for each field across all PDFs
        var aggregatedData = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

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
                // Iterate over each form field and capture its value
                foreach (Field field in doc.Form.Fields)
                {
                    // The field name (partial name) is the key we use for aggregation
                    string fieldName = field.PartialName;
                    // Most field types expose a Value property; fallback to empty string if null
                    string fieldValue = field?.Value?.ToString() ?? string.Empty;

                    if (!aggregatedData.ContainsKey(fieldName))
                        aggregatedData[fieldName] = new List<string>();

                    aggregatedData[fieldName].Add(fieldValue);
                }
            }
        }

        // Prepare the final JSON where each field maps to an array of its collected values
        var finalJson = JsonSerializer.Serialize(aggregatedData, new JsonSerializerOptions { WriteIndented = true });

        // Save the aggregated JSON to the output file (lifecycle rule: use Document.Save only for PDFs;
        // here we are writing a plain file, so File.WriteAllText is appropriate)
        File.WriteAllText(outputJsonPath, finalJson);

        Console.WriteLine($"Aggregated form data saved to '{outputJsonPath}'.");
    }
}
