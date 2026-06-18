using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "JsonChunks";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF form using Aspose.Pdf.Facades.Form
        using (Form form = new Form(inputPdf))
        {
            // Export all form fields to a JSON stream in memory
            using (MemoryStream jsonStream = new MemoryStream())
            {
                form.ExportJson(jsonStream, true);
                jsonStream.Position = 0;

                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string jsonText = reader.ReadToEnd();

                    // Parse the JSON to split into chunks of at most 100 fields
                    using (JsonDocument doc = JsonDocument.Parse(jsonText))
                    {
                        JsonElement root = doc.RootElement;
                        if (root.ValueKind != JsonValueKind.Object)
                        {
                            Console.Error.WriteLine("Unexpected JSON format.");
                            return;
                        }

                        // Collect all field name/value pairs
                        List<KeyValuePair<string, JsonElement>> fields = new List<KeyValuePair<string, JsonElement>>();
                        foreach (JsonProperty prop in root.EnumerateObject())
                        {
                            fields.Add(new KeyValuePair<string, JsonElement>(prop.Name, prop.Value));
                        }

                        const int batchSize = 100;
                        int totalBatches = (fields.Count + batchSize - 1) / batchSize;

                        // Write each batch to a separate JSON file
                        for (int i = 0; i < totalBatches; i++)
                        {
                            var batchDict = new Dictionary<string, JsonElement>();
                            int start = i * batchSize;
                            int end = Math.Min(start + batchSize, fields.Count);
                            for (int j = start; j < end; j++)
                            {
                                batchDict[fields[j].Key] = fields[j].Value;
                            }

                            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                            string batchJson = JsonSerializer.Serialize(batchDict, options);
                            string outPath = Path.Combine(outputDir, $"form_part_{i + 1}.json");
                            File.WriteAllText(outPath, batchJson);
                        }

                        Console.WriteLine($"Exported {fields.Count} fields into {totalBatches} JSON files.");
                    }
                }
            }
        }
    }
}