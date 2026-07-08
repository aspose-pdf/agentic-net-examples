using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "JsonChunks";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF form using Aspose.Pdf.Facades.Form
        using (Form form = new Form(inputPdfPath))
        {
            // Export all form fields to a JSON stream in memory
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // ExportJson writes indented JSON by default (second argument = true)
                form.ExportJson(jsonStream, true);
                jsonStream.Position = 0;

                // Read the JSON text
                string jsonText;
                using (StreamReader sr = new StreamReader(jsonStream))
                {
                    jsonText = sr.ReadToEnd();
                }

                // Parse the JSON object
                using (JsonDocument doc = JsonDocument.Parse(jsonText))
                {
                    JsonElement root = doc.RootElement;
                    if (root.ValueKind != JsonValueKind.Object)
                    {
                        Console.Error.WriteLine("Unexpected JSON format: root element is not an object.");
                        return;
                    }

                    // Collect all field properties into a list for easy chunking
                    List<JsonProperty> allFields = new List<JsonProperty>();
                    foreach (JsonProperty prop in root.EnumerateObject())
                    {
                        allFields.Add(prop);
                    }

                    int totalFields = allFields.Count;
                    int chunkIndex = 1;

                    // Split into chunks of at most 100 fields
                    for (int start = 0; start < totalFields; start += 100)
                    {
                        int count = Math.Min(100, totalFields - start);
                        string chunkPath = Path.Combine(outputFolder, $"form_part_{chunkIndex}.json");

                        using (FileStream fs = new FileStream(chunkPath, FileMode.Create, FileAccess.Write))
                        using (Utf8JsonWriter writer = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true }))
                        {
                            writer.WriteStartObject();

                            for (int i = 0; i < count; i++)
                            {
                                JsonProperty field = allFields[start + i];
                                writer.WritePropertyName(field.Name);
                                field.Value.WriteTo(writer);
                            }

                            writer.WriteEndObject();
                        }

                        Console.WriteLine($"Created: {chunkPath} (fields {start + 1}‑{start + count})");
                        chunkIndex++;
                    }
                }
            }
        }

        Console.WriteLine("Form data export and splitting completed.");
    }
}