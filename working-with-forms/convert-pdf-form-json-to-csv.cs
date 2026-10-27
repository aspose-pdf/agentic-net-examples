using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string jsonPath = "formdata.json";
        const string csvPath = "formdata.csv";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Read the entire JSON file
        string jsonContent;
        using (StreamReader reader = new StreamReader(jsonPath))
        {
            jsonContent = reader.ReadToEnd();
        }

        // Deserialize JSON into a list of dictionaries (field name -> value)
        List<Dictionary<string, JsonElement>> records;
        try
        {
            records = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonContent);
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        if (records == null || records.Count == 0)
        {
            Console.WriteLine("No data to convert.");
            return;
        }

        // Use the keys from the first record as CSV headers
        var headers = new List<string>(records[0].Keys);

        // Write CSV file
        using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            // Header line
            writer.WriteLine(string.Join(",", EscapeCsvValues(headers)));

            // Data rows
            foreach (var record in records)
            {
                var row = new List<string>();
                foreach (var header in headers)
                {
                    if (record.TryGetValue(header, out JsonElement value))
                        row.Add(ConvertJsonElementToString(value));
                    else
                        row.Add(string.Empty);
                }
                writer.WriteLine(string.Join(",", EscapeCsvValues(row)));
            }
        }

        Console.WriteLine($"CSV file created at: {csvPath}");
    }

    // Convert a JsonElement to a plain string suitable for CSV
    private static string ConvertJsonElementToString(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.GetRawText(),
            JsonValueKind.True => "true",
            JsonValueKind.False => "false",
            JsonValueKind.Null => string.Empty,
            JsonValueKind.Object => element.GetRawText(),
            JsonValueKind.Array => element.GetRawText(),
            _ => element.GetRawText()
        };
    }

    // Escape CSV values that contain commas, quotes, or newlines
    private static IEnumerable<string> EscapeCsvValues(IEnumerable<string> values)
    {
        foreach (var v in values)
        {
            if (v == null)
            {
                yield return string.Empty;
                continue;
            }

            bool mustQuote = v.Contains(',') || v.Contains('\"') || v.Contains('\n') || v.Contains('\r');
            if (mustQuote)
            {
                string escaped = v.Replace("\"", "\"\"");
                yield return $"\"{escaped}\"";
            }
            else
            {
                yield return v;
            }
        }
    }
}