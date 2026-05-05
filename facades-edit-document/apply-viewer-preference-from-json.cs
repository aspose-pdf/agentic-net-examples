using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfViewerPreferenceApplier
{
    // Represents a single entry in the JSON configuration.
    public class ViewerConfig
    {
        public string InputPath { get; set; }          // Path to the source PDF.
        public string OutputPath { get; set; }         // Path where the modified PDF will be saved.
        public int ViewerAttribution { get; set; }     // Integer value of ViewerPreference flags.
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path of the JSON configuration file.
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Usage: PdfViewerPreferenceApplier <config.json>");
                return;
            }

            string configFile = args[0];
            if (!File.Exists(configFile))
            {
                Console.Error.WriteLine($"Configuration file not found: {configFile}");
                return;
            }

            // Deserialize the JSON file into a list of ViewerConfig objects.
            List<ViewerConfig> configs;
            try
            {
                string json = File.ReadAllText(configFile);
                configs = JsonSerializer.Deserialize<List<ViewerConfig>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            if (configs == null || configs.Count == 0)
            {
                Console.WriteLine("No configuration entries found.");
                return;
            }

            // Process each PDF according to its configuration.
            foreach (var cfg in configs)
            {
                if (!File.Exists(cfg.InputPath))
                {
                    Console.Error.WriteLine($"Input PDF not found: {cfg.InputPath}");
                    continue;
                }

                try
                {
                    // PdfContentEditor does not implement IDisposable, so we instantiate it directly.
                    PdfContentEditor editor = new PdfContentEditor();

                    // Bind the source PDF.
                    editor.BindPdf(cfg.InputPath);

                    // Apply the viewer preference flags.
                    editor.ChangeViewerPreference(cfg.ViewerAttribution);

                    // Save the modified PDF.
                    editor.Save(cfg.OutputPath);

                    // Optional: retrieve the applied preferences (demonstration purpose).
                    int applied = editor.GetViewerPreference();
                    Console.WriteLine($"Processed '{cfg.InputPath}' -> '{cfg.OutputPath}'. Applied flags: 0x{applied:X8}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{cfg.InputPath}': {ex.Message}");
                }
            }
        }
    }
}