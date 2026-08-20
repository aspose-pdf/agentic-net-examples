using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf; // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        const string jsonPath = "formdata.json";   // Input JSON file exported from a PDF form
        const string csvPath  = "formdata.csv";    // Output CSV file

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Read the entire JSON content
            string jsonContent = File.ReadAllText(jsonPath);

            // Parse the JSON document
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("FieldName,Value");

                JsonElement root = doc.RootElement;

                // Case 1: JSON is an object where each property is a field name
                if (root.ValueKind == JsonValueKind.Object)
                {
                    foreach (JsonProperty prop in root.EnumerateObject())
                    {
                        string field = EscapeCsv(prop.Name);
                        string value = EscapeCsv(prop.Value.GetString() ?? prop.Value.ToString());
                        writer.WriteLine($"{field},{value}");
                    }
                }
                // Case 2: JSON is an array of objects (e.g., [{ "FullName":"Name", "Value":"John" }, ...])
                else if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        string field = "";
                        string value = "";

                        if (element.TryGetProperty("FullName", out JsonElement nameProp) ||
                            element.TryGetProperty("Name", out nameProp))
                        {
                            field = nameProp.GetString() ?? "";
                        }

                        if (element.TryGetProperty("Value", out JsonElement valueProp))
                        {
                            value = valueProp.GetString() ?? valueProp.ToString();
                        }
                        else
                        {
                            // Fallback: serialize the whole element as a string
                            value = element.GetRawText();
                        }

                        writer.WriteLine($"{EscapeCsv(field)},{EscapeCsv(value)}");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Unsupported JSON structure for conversion.");
                }
            }

            Console.WriteLine($"CSV file created successfully at '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    // Escapes a CSV field according to RFC 4180
    static string EscapeCsv(string input)
    {
        if (input == null) return "";

        bool mustQuote = input.Contains('"') || input.Contains(',') ||
                         input.Contains('\n') || input.Contains('\r');

        if (mustQuote)
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        return input;
    }
}