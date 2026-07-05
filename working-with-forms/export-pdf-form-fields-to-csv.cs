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
        const string pdfPath = "input.pdf";
        const string csvPath = "output.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load within using)
        using (Document doc = new Document(pdfPath))
        {
            // Export form fields to JSON using a memory stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                doc.Form.ExportToJson(jsonStream);
                jsonStream.Position = 0;

                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string json = reader.ReadToEnd();
                    WriteCsvFromJson(json, csvPath);
                }
            }
        }

        Console.WriteLine($"CSV saved to '{csvPath}'.");
    }

    // Parses the JSON exported by Aspose.Pdf and writes a simple CSV file
    static void WriteCsvFromJson(string json, string csvPath)
    {
        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            JsonElement root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Array)
            {
                Console.Error.WriteLine("Unexpected JSON format: root element is not an array.");
                return;
            }

            var fieldNames = new List<string>();
            var fieldValues = new List<string>();

            foreach (JsonElement element in root.EnumerateArray())
            {
                // Aspose.Pdf typically uses "FullName" for the field name and "Value" for its content
                string name = element.GetProperty("FullName").GetString() ?? string.Empty;
                string value = element.GetProperty("Value").GetString() ?? string.Empty;

                fieldNames.Add(name);
                fieldValues.Add(value);
            }

            // Write CSV (lifecycle: save using StreamWriter)
            using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
            {
                writer.WriteLine(string.Join(",", EscapeCsv(fieldNames)));
                writer.WriteLine(string.Join(",", EscapeCsv(fieldValues)));
            }
        }
    }

    // Escapes CSV fields according to RFC 4180
    static IEnumerable<string> EscapeCsv(IEnumerable<string> fields)
    {
        foreach (var field in fields)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                string escaped = field.Replace("\"", "\"\"");
                yield return $"\"{escaped}\"";
            }
            else
            {
                yield return field;
            }
        }
    }
}