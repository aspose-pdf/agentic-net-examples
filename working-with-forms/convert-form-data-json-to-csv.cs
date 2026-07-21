using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Paths for input JSON (exported form data) and output CSV
        const string jsonPath = "formdata.json";
        const string csvPath  = "formdata.csv";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Read the entire JSON content
        string jsonContent = File.ReadAllText(jsonPath);

        // Parse JSON; expect an array of objects where each object represents a form record
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        JsonElement root = doc.RootElement;

        if (root.ValueKind != JsonValueKind.Array)
        {
            Console.Error.WriteLine("Invalid JSON format: expected an array of objects.");
            return;
        }

        // Determine CSV headers from the first object in the array
        var headers = new List<string>();
        if (root.GetArrayLength() > 0)
        {
            foreach (JsonProperty prop in root[0].EnumerateObject())
                headers.Add(prop.Name);
        }

        // Write CSV file
        using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            // Header line
            writer.WriteLine(string.Join(",", headers));

            // Data rows
            foreach (JsonElement element in root.EnumerateArray())
            {
                var values = new List<string>();
                foreach (string header in headers)
                {
                    if (element.TryGetProperty(header, out JsonElement val))
                    {
                        // Convert value to string and escape as needed for CSV
                        string text = val.ToString().Replace("\"", "\"\"");
                        if (text.Contains(",") || text.Contains("\"") || text.Contains("\n"))
                            text = $"\"{text}\"";
                        values.Add(text);
                    }
                    else
                    {
                        values.Add(string.Empty);
                    }
                }
                writer.WriteLine(string.Join(",", values));
            }
        }

        Console.WriteLine($"CSV file successfully created at '{csvPath}'.");
    }
}