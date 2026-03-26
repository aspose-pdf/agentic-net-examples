using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // List of JSON files exported from individual PDFs
        string[] inputFiles = new string[] { "data1.json", "data2.json", "data3.json" };
        List<JsonElement> combinedElements = new List<JsonElement>();

        foreach (string filePath in inputFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            string fileContent = File.ReadAllText(filePath);
            using (JsonDocument document = JsonDocument.Parse(fileContent))
            {
                JsonElement root = document.RootElement;
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        combinedElements.Add(element.Clone());
                    }
                }
                else
                {
                    combinedElements.Add(root.Clone());
                }
            }
        }

        // Serialize the combined list as a JSON array
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string combinedJson = JsonSerializer.Serialize(combinedElements, options);

        // Write the merged JSON array to a file
        string outputPath = "combined.json";
        File.WriteAllText(outputPath, combinedJson);
        Console.WriteLine($"Combined JSON written to '{outputPath}'.");
    }
}