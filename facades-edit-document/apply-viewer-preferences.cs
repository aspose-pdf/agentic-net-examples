using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string configPath = "config.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        string jsonText = File.ReadAllText(configPath);
        using (JsonDocument jsonDoc = JsonDocument.Parse(jsonText))
        {
            JsonElement root = jsonDoc.RootElement;

            List<string> pdfFiles = new List<string>();
            if (root.TryGetProperty("files", out JsonElement filesElement) && filesElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement fileElem in filesElement.EnumerateArray())
                {
                    if (fileElem.ValueKind == JsonValueKind.String)
                    {
                        string filePath = fileElem.GetString();
                        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                        {
                            pdfFiles.Add(filePath);
                        }
                    }
                }
            }

            List<string> prefNames = new List<string>();
            if (root.TryGetProperty("preferences", out JsonElement prefElement) && prefElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement prefElem in prefElement.EnumerateArray())
                {
                    if (prefElem.ValueKind == JsonValueKind.String)
                    {
                        string prefName = prefElem.GetString();
                        if (!string.IsNullOrEmpty(prefName))
                        {
                            prefNames.Add(prefName);
                        }
                    }
                }
            }

            // Build a map from preference name to its integer flag value using reflection
            Dictionary<string, int> prefMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            FieldInfo[] fields = typeof(ViewerPreference).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(int))
                {
                    prefMap[field.Name] = (int)field.GetValue(null);
                }
            }

            foreach (string inputPath in pdfFiles)
            {
                string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_out.pdf";

                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(inputPath);

                foreach (string prefName in prefNames)
                {
                    if (prefMap.TryGetValue(prefName, out int prefValue))
                    {
                        editor.ChangeViewerPreference(prefValue);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Unknown viewer preference: {prefName}");
                    }
                }

                editor.Save(outputFileName);
                Console.WriteLine($"Processed '{inputPath}' -> '{outputFileName}'");
            }
        }
    }
}