using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Permissions, CryptoAlgorithm

namespace BatchPdfEncryption
{
    // Represents a single encryption task read from the configuration file
    public class EncryptionTask
    {
        public string InputPath { get; set; }          // Path to the source PDF
        public string OutputPath { get; set; }         // Desired path for the encrypted PDF
        public string UserPassword { get; set; }       // Password required to open the PDF
        public string OwnerPassword { get; set; }      // Password required to change permissions
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path of the JSON configuration file
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Usage: BatchPdfEncryption <config.json>");
                return;
            }

            string configFile = args[0];
            if (!File.Exists(configFile))
            {
                Console.Error.WriteLine($"Configuration file not found: {configFile}");
                return;
            }

            List<EncryptionTask> tasks;
            try
            {
                // Load and deserialize the JSON configuration
                string json = File.ReadAllText(configFile);
                tasks = JsonSerializer.Deserialize<List<EncryptionTask>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (tasks == null)
                {
                    Console.Error.WriteLine("Configuration file is empty or malformed.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Process each encryption task
            foreach (var task in tasks)
            {
                try
                {
                    // Validate input file existence
                    if (!File.Exists(task.InputPath))
                    {
                        Console.Error.WriteLine($"Input PDF not found: {task.InputPath}");
                        continue;
                    }

                    // Ensure the output directory exists
                    string outputDir = Path.GetDirectoryName(task.OutputPath);
                    if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    // Load the PDF, encrypt it, and save the result
                    using (Document doc = new Document(task.InputPath))
                    {
                        // Define desired permissions (example: allow printing and content extraction)
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                        // Encrypt using AES-256 (preferred algorithm)
                        doc.Encrypt(
                            userPassword: task.UserPassword,
                            ownerPassword: task.OwnerPassword,
                            permissions: perms,
                            cryptoAlgorithm: CryptoAlgorithm.AESx256);

                        // Save the encrypted PDF
                        doc.Save(task.OutputPath);
                    }

                    Console.WriteLine($"SUCCESS: Encrypted '{task.InputPath}' -> '{task.OutputPath}'");
                }
                catch (Exception ex)
                {
                    // Log any error that occurs during processing of this task
                    Console.Error.WriteLine($"ERROR processing '{task.InputPath}': {ex.Message}");
                }
            }
        }
    }
}