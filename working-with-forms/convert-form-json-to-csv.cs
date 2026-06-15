using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf; // Core Aspose.Pdf namespace (no Facades)

class FormJsonToCsvConverter
{
    /// <summary>
    /// Converts a JSON file that contains form field data into a CSV file.
    /// The JSON can be either a single object (fieldName : value) or an array of such objects.
    /// The resulting CSV will have a header row with field names and subsequent rows with values.
    /// </summary>
    /// <param name="jsonFilePath">Path to the input JSON file.</param>
    /// <param name="csvFilePath">Path where the CSV file will be written.</param>
    public static void ConvertJsonToCsv(string jsonFilePath, string csvFilePath)
    {
        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonFilePath}");
            return;
        }

        // Read the entire JSON content
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Parse the JSON document
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        JsonElement root = doc.RootElement;

        // Determine if the root is an array of records or a single record
        bool isArray = root.ValueKind == JsonValueKind.Array;

        // Prepare a list of records (each record is a dictionary of field/value)
        var records = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>();

        if (isArray)
        {
            foreach (JsonElement element in root.EnumerateArray())
            {
                records.Add(JsonElementToDictionary(element));
            }
        }
        else if (root.ValueKind == JsonValueKind.Object)
        {
            records.Add(JsonElementToDictionary(root));
        }
        else
        {
            Console.Error.WriteLine("Unsupported JSON structure. Expected an object or an array of objects.");
            return;
        }

        if (records.Count == 0)
        {
            Console.WriteLine("No data found in JSON.");
            return;
        }

        // Build the CSV header from the union of all keys
        var allKeys = new System.Collections.Generic.SortedSet<string>();
        foreach (var rec in records)
        {
            foreach (var key in rec.Keys)
                allKeys.Add(key);
        }

        // Write CSV
        using var writer = new StreamWriter(csvFilePath, false, Encoding.UTF8);
        // Header
        writer.WriteLine(string.Join(",", allKeys));

        // Rows
        foreach (var rec in records)
        {
            var row = new StringBuilder();
            bool first = true;
            foreach (var key in allKeys)
            {
                if (!first) row.Append(',');
                first = false;

                rec.TryGetValue(key, out string value);
                // Escape double quotes by doubling them and wrap the field in quotes if it contains a comma or quote
                if (value != null && (value.Contains(',') || value.Contains('\"') || value.Contains('\n')))
                {
                    string escaped = value.Replace("\"", "\"\"");
                    row.Append('\"').Append(escaped).Append('\"');
                }
                else
                {
                    row.Append(value);
                }
            }
            writer.WriteLine(row.ToString());
        }

        Console.WriteLine($"CSV file created at: {csvFilePath}");
    }

    /// <summary>
    /// Converts a JsonElement representing an object into a dictionary of string values.
    /// Nested objects/arrays are serialized to their JSON string representation.
    /// </summary>
    private static System.Collections.Generic.Dictionary<string, string> JsonElementToDictionary(JsonElement element)
    {
        var dict = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (element.ValueKind != JsonValueKind.Object)
            return dict;

        foreach (JsonProperty prop in element.EnumerateObject())
        {
            string value;
            switch (prop.Value.ValueKind)
            {
                case JsonValueKind.String:
                    value = prop.Value.GetString();
                    break;
                case JsonValueKind.Number:
                    value = prop.Value.GetRawText(); // preserve formatting
                    break;
                case JsonValueKind.True:
                case JsonValueKind.False:
                    value = prop.Value.GetBoolean().ToString();
                    break;
                case JsonValueKind.Null:
                    value = string.Empty;
                    break;
                default:
                    // For complex types (objects, arrays) store the raw JSON fragment
                    value = prop.Value.GetRawText();
                    break;
            }
            dict[prop.Name] = value;
        }
        return dict;
    }

    // Example usage
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "sample_form.pdf";   // Input PDF (optional, shown for context)
        const string jsonPath = "form_data.json";   // JSON exported from the PDF form
        const string csvPath = "form_data.csv";     // Desired CSV output

        // OPTIONAL: If you need to export the form data from a PDF to JSON first,
        // uncomment the following block. This uses the core Aspose.Pdf Form API.
        /*
        using (Document pdfDoc = new Document(pdfPath))
        using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
        {
            // Export all form fields to JSON (button values are omitted by design)
            pdfDoc.Form.ExportToJson(jsonStream);
        }
        */

        // Convert the JSON file to CSV
        ConvertJsonToCsv(jsonPath, csvPath);
    }
}