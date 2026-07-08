using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace BatchPdfDecryption
{
    // Simple POCO to hold configuration for each file
    public class DecryptConfig
    {
        public string InputPath { get; set; }          // Path to the encrypted PDF
        public string OwnerPassword { get; set; }      // Owner password for decryption
        public string OutputPath { get; set; }         // Path where the decrypted PDF will be saved
    }

    class Program
    {
        static void Main()
        {
            const string configFile = "decryptConfig.json";

            if (!File.Exists(configFile))
            {
                Console.Error.WriteLine($"Configuration file not found: {configFile}");
                return;
            }

            // Load configuration (JSON array of DecryptConfig objects)
            List<DecryptConfig> configs;
            try
            {
                string json = File.ReadAllText(configFile);
                configs = JsonSerializer.Deserialize<List<DecryptConfig>>(json);
                if (configs == null || configs.Count == 0)
                {
                    Console.Error.WriteLine("Configuration is empty or invalid.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            foreach (var cfg in configs)
            {
                // Validate paths
                if (string.IsNullOrWhiteSpace(cfg.InputPath) ||
                    string.IsNullOrWhiteSpace(cfg.OwnerPassword) ||
                    string.IsNullOrWhiteSpace(cfg.OutputPath))
                {
                    Console.Error.WriteLine("Invalid configuration entry; skipping.");
                    continue;
                }

                if (!File.Exists(cfg.InputPath))
                {
                    Console.Error.WriteLine($"Input file not found: {cfg.InputPath}");
                    continue;
                }

                try
                {
                    // PdfFileSecurity handles opening, decrypting, and saving the file.
                    // It implements IDisposable, so we wrap it in a using block.
                    using (PdfFileSecurity security = new PdfFileSecurity(cfg.InputPath, cfg.OutputPath))
                    {
                        bool success = security.DecryptFile(cfg.OwnerPassword);
                        if (success)
                        {
                            Console.WriteLine($"Decrypted: '{cfg.InputPath}' → '{cfg.OutputPath}'");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Decryption failed for: {cfg.InputPath}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{cfg.InputPath}': {ex.Message}");
                }
            }
        }
    }
}