using System;
using System.Collections.Generic;
using System.IO;
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
        // Output JSON file that will contain the aggregated data
        const string outputJson = "merged_form_data.json";

        // Dictionary to hold aggregated field values.
        // Key = field name, Value = list of values from each PDF.
        var aggregatedData = new Dictionary<string, List<object>>(StringComparer.OrdinalIgnoreCase);

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // Bind the Form facade to the loaded document.
                using (Form form = new Form(doc))
                {
                    // Export the form fields to a JSON stream.
                    using (MemoryStream jsonStream = new MemoryStream())
                    {
                        // 'false' – do not include empty fields (adjust as needed).
                        form.ExportJson(jsonStream, false);
                        jsonStream.Position = 0;

                        // Read the JSON text.
                        string jsonText = new StreamReader(jsonStream, Encoding.UTF8).ReadToEnd();

                        // Deserialize into a dictionary of field name/value pairs.
                        var fields = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonText);

                        if (fields != null)
                        {
                            foreach (var kvp in fields)
                            {
                                // Convert JsonElement to a .NET primitive (string, number, bool, etc.).
                                object value = ConvertJsonElement(kvp.Value);

                                if (!aggregatedData.TryGetValue(kvp.Key, out var list))
                                {
                                    list = new List<object>();
                                    aggregatedData[kvp.Key] = list;
                                }
                                list.Add(value);
                            }
                        }
                    }
                }
            }
        }

        // Prepare the final structure: each field maps to an array of its collected values.
        var finalJson = new Dictionary<string, object>();
        foreach (var kvp in aggregatedData)
        {
            finalJson[kvp.Key] = kvp.Value;
        }

        // Serialize the aggregated data to JSON and write to the output file.
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string aggregatedJson = JsonSerializer.Serialize(finalJson, jsonOptions);
        File.WriteAllText(outputJson, aggregatedJson, Encoding.UTF8);

        Console.WriteLine($"Aggregated form data saved to '{outputJson}'.");
    }

    // Helper to convert a JsonElement to a plain .NET object.
    private static object ConvertJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.String:
                return element.GetString() ?? string.Empty;
            case JsonValueKind.Number:
                if (element.TryGetInt64(out long l))
                    return l;
                if (element.TryGetDouble(out double d))
                    return d;
                return element.GetDecimal();
            case JsonValueKind.True:
                return true;
            case JsonValueKind.False:
                return false;
            case JsonValueKind.Null:
                return null;
            case JsonValueKind.Object:
            case JsonValueKind.Array:
                // For complex types, return the raw JSON string.
                return element.GetRawText();
            default:
                return element.GetRawText();
        }
    }
}