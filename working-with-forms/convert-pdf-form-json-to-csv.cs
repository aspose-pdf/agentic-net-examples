using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string jsonPath = "formData.json";   // input JSON file containing extracted form data
        const string csvPath  = "formData.csv";    // output CSV file

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Input file not found: {jsonPath}");
            return;
        }

        // Read the entire JSON content
        string jsonContent;
        using (StreamReader reader = new StreamReader(jsonPath, Encoding.UTF8))
        {
            jsonContent = reader.ReadToEnd();
        }

        // Parse JSON as an array of objects
        JsonDocument doc;
        try
        {
            doc = JsonDocument.Parse(jsonContent);
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Invalid JSON format: {ex.Message}");
            return;
        }

        if (doc.RootElement.ValueKind != JsonValueKind.Array)
        {
            Console.Error.WriteLine("Expected JSON array at the root.");
            return;
        }

        // Collect rows as dictionaries (field name -> value)
        var rows = new List<Dictionary<string, string>>();
        var headerSet = new SortedSet<string>(); // to keep column order consistent

        foreach (JsonElement element in doc.RootElement.EnumerateArray())
        {
            if (element.ValueKind != JsonValueKind.Object)
                continue; // skip non‑object entries

            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (JsonProperty prop in element.EnumerateObject())
            {
                string value = prop.Value.ValueKind switch
                {
                    JsonValueKind.String  => prop.Value.GetString(),
                    JsonValueKind.Number  => prop.Value.GetRawText(),
                    JsonValueKind.True    => "true",
                    JsonValueKind.False   => "false",
                    JsonValueKind.Null    => string.Empty,
                    _                      => prop.Value.GetRawText()
                };
                dict[prop.Name] = value ?? string.Empty;
                headerSet.Add(prop.Name);
            }
            rows.Add(dict);
        }

        // Write CSV
        using (StreamWriter writer = new StreamWriter(csvPath, false, new UTF8Encoding(true)))
        {
            // Write header line
            var header = new List<string>(headerSet);
            writer.WriteLine(string.Join(",", EscapeCsvFields(header)));

            // Write each row
            foreach (var row in rows)
            {
                var fields = new List<string>();
                foreach (string col in header)
                {
                    row.TryGetValue(col, out string val);
                    fields.Add(val ?? string.Empty);
                }
                writer.WriteLine(string.Join(",", EscapeCsvFields(fields)));
            }
        }

        Console.WriteLine($"Conversion completed. CSV saved to '{csvPath}'.");
    }

    // Escape fields that contain commas, quotes or line breaks according to CSV rules
    private static IEnumerable<string> EscapeCsvFields(IEnumerable<string> fields)
    {
        foreach (var field in fields)
        {
            if (field.Contains(',') || field.Contains('\"') || field.Contains('\n') || field.Contains('\r'))
            {
                // Double up any existing quotes and wrap the field in quotes
                string escaped = $"\"{field.Replace("\"", "\"\"")}\"";
                yield return escaped;
            }
            else
            {
                yield return field;
            }
        }
    }
}