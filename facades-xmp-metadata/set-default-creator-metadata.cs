using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string defaultCreatorToolFallback = "MyApplication";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine the default CreatorTool value.
        // 1. Try to read from appsettings.json (if present).
        // 2. Override with an environment variable named "DefaultCreatorTool" if it exists.
        // 3. Fallback to the hard‑coded default.
        string defaultCreatorTool = GetDefaultCreatorTool(defaultCreatorToolFallback);

        // Load the PDF document. Aspose.Pdf.Document provides access to metadata via the Info property.
        using (var doc = new Document(inputPath))
        {
            // If the Creator metadata is missing or empty, set the default value.
            if (string.IsNullOrWhiteSpace(doc.Info.Creator))
            {
                doc.Info.Creator = defaultCreatorTool;
            }

            // Save the updated PDF to a new file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Creator set to '{defaultCreatorTool}' at '{outputPath}'.");
    }

    private static string GetDefaultCreatorTool(string fallback)
    {
        // Start with the fallback value.
        string result = fallback;

        // 1. Attempt to read from appsettings.json.
        const string configFile = "appsettings.json";
        if (File.Exists(configFile))
        {
            try
            {
                string json = File.ReadAllText(configFile);
                using JsonDocument doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("DefaultCreatorTool", out JsonElement element))
                {
                    string? value = element.GetString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        result = value;
                    }
                }
            }
            catch (Exception ex)
            {
                // If the JSON is malformed or another error occurs, ignore it and keep the current result.
                Console.Error.WriteLine($"Warning: could not read '{configFile}': {ex.Message}");
            }
        }

        // 2. Override with environment variable if it exists.
        string? env = Environment.GetEnvironmentVariable("DefaultCreatorTool");
        if (!string.IsNullOrWhiteSpace(env))
        {
            result = env;
        }

        return result;
    }
}
