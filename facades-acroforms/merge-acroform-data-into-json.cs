using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files containing form fields
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file that will contain aggregated form data
        const string outputJsonPath = "merged_form_data.json";

        // Dictionary to hold aggregated values:
        // key = field name, value = list of values from each PDF
        var aggregatedData = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(pdfPath))
            {
                // Initialize the Form facade and bind the document (facade rule)
                using (Form form = new Form())
                {
                    form.BindPdf(doc);

                    // Export form fields to JSON via a memory stream
                    using (MemoryStream jsonStream = new MemoryStream())
                    {
                        // ExportJson(bool) – true to include empty fields
                        form.ExportJson(jsonStream, true);
                        jsonStream.Position = 0;

                        // Read the JSON text
                        string jsonText = Encoding.UTF8.GetString(jsonStream.ToArray());

                        // Deserialize into a simple dictionary (fieldName -> fieldValue)
                        var fieldDict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);

                        if (fieldDict != null)
                        {
                            foreach (var kvp in fieldDict)
                            {
                                if (!aggregatedData.TryGetValue(kvp.Key, out var list))
                                {
                                    list = new List<string>();
                                    aggregatedData[kvp.Key] = list;
                                }
                                list.Add(kvp.Value);
                            }
                        }
                    }
                }
            }
        }

        // Prepare final JSON structure: each field maps to an array of its collected values
        var finalJson = new Dictionary<string, string[]>();
        foreach (var kvp in aggregatedData)
        {
            finalJson[kvp.Key] = kvp.Value.ToArray();
        }

        // Serialize the aggregated result with indentation for readability
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string resultJson = JsonSerializer.Serialize(finalJson, options);

        // Write the aggregated JSON to the output file
        File.WriteAllText(outputJsonPath, resultJson, Encoding.UTF8);

        Console.WriteLine($"Aggregated form data saved to '{outputJsonPath}'.");
    }
}