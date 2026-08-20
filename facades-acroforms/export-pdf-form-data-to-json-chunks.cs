using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "JsonChunks";
        const int maxFieldsPerFile = 100;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF form using the Form facade
        using (Form form = new Form(inputPdfPath))
        {
            // Export all form fields to a JSON stream
            using (MemoryStream fullJsonStream = new MemoryStream())
            {
                // indented = true for readability
                form.ExportJson(fullJsonStream, true);
                fullJsonStream.Position = 0;

                // Read the exported JSON as a string
                string fullJson;
                using (StreamReader reader = new StreamReader(fullJsonStream))
                {
                    fullJson = reader.ReadToEnd();
                }

                // Parse the JSON document
                using (JsonDocument jsonDoc = JsonDocument.Parse(fullJson))
                {
                    // The exported JSON is an object where each property represents a field
                    JsonElement root = jsonDoc.RootElement;
                    if (root.ValueKind != JsonValueKind.Object)
                    {
                        Console.Error.WriteLine("Unexpected JSON format: root element is not an object.");
                        return;
                    }

                    // Collect all field properties
                    List<JsonProperty> allFields = root.EnumerateObject().ToList();

                    // Split the fields into chunks of at most maxFieldsPerFile
                    int chunkCount = (allFields.Count + maxFieldsPerFile - 1) / maxFieldsPerFile;
                    for (int chunkIndex = 0; chunkIndex < chunkCount; chunkIndex++)
                    {
                        IEnumerable<JsonProperty> chunkFields = allFields
                            .Skip(chunkIndex * maxFieldsPerFile)
                            .Take(maxFieldsPerFile);

                        // Write the chunk to a new JSON file
                        string chunkFilePath = Path.Combine(
                            outputDirectory,
                            $"form_chunk_{chunkIndex + 1}.json");

                        using (MemoryStream chunkStream = new MemoryStream())
                        using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(
                            chunkStream,
                            new JsonWriterOptions { Indented = true }))
                        {
                            jsonWriter.WriteStartObject();

                            foreach (JsonProperty field in chunkFields)
                            {
                                jsonWriter.WritePropertyName(field.Name);
                                field.Value.WriteTo(jsonWriter);
                            }

                            jsonWriter.WriteEndObject();
                            jsonWriter.Flush();

                            // Save the chunk to disk
                            File.WriteAllBytes(chunkFilePath, chunkStream.ToArray());
                        }

                        Console.WriteLine($"Created chunk file: {chunkFilePath}");
                    }
                }
            }
        }
    }
}