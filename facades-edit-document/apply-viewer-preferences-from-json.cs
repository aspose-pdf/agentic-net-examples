using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.Pdf.Facades;

namespace PdfViewerPreferenceApplier
{
    // Represents the overall configuration file structure.
    public class Config
    {
        public List<FileConfig> Files { get; set; }
    }

    // Represents a single PDF processing entry.
    public class FileConfig
    {
        public string InputPath { get; set; }      // Path to the source PDF.
        public string OutputPath { get; set; }     // Desired output PDF path.
        public List<string> Preferences { get; set; } // List of ViewerPreference flag names.
    }

    class Program
    {
        static void Main()
        {
            const string jsonConfigPath = "viewerPreferences.json";

            if (!File.Exists(jsonConfigPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {jsonConfigPath}");
                return;
            }

            // Deserialize the JSON configuration.
            Config config;
            try
            {
                string json = File.ReadAllText(jsonConfigPath);
                config = JsonSerializer.Deserialize<Config>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            if (config?.Files == null || config.Files.Count == 0)
            {
                Console.WriteLine("No files to process.");
                return;
            }

            // Process each PDF according to its specified viewer preferences.
            foreach (var file in config.Files)
            {
                if (!File.Exists(file.InputPath))
                {
                    Console.Error.WriteLine($"Input PDF not found: {file.InputPath}");
                    continue;
                }

                try
                {
                    // Create the PdfContentEditor facade.
                    PdfContentEditor editor = new PdfContentEditor();

                    // Bind the source PDF.
                    editor.BindPdf(file.InputPath);

                    // Combine the requested ViewerPreference flags.
                    int combinedPref = 0;
                    if (file.Preferences != null)
                    {
                        foreach (string prefName in file.Preferences)
                        {
                            // Use reflection to obtain the constant value from ViewerPreference.
                            FieldInfo field = typeof(ViewerPreference).GetField(prefName,
                                BindingFlags.Public | BindingFlags.Static);
                            if (field != null && field.FieldType == typeof(int))
                            {
                                combinedPref |= (int)field.GetValue(null);
                            }
                            else
                            {
                                Console.Error.WriteLine($"Unknown ViewerPreference: {prefName}");
                            }
                        }
                    }

                    // Apply the combined viewer preference.
                    editor.ChangeViewerPreference(combinedPref);

                    // Save the modified PDF.
                    editor.Save(file.OutputPath);

                    // Close the facade (PdfContentEditor does not implement IDisposable).
                    editor.Close();

                    Console.WriteLine($"Processed '{file.InputPath}' -> '{file.OutputPath}'");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{file.InputPath}': {ex.Message}");
                }
            }
        }
    }
}