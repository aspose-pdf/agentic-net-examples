using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

namespace BatchPdfEncryption
{
    // Model representing a single encryption task read from the configuration file
    public class EncryptionTask
    {
        public string InputPath { get; set; }      // Path to the source PDF
        public string OutputPath { get; set; }     // Path where the encrypted PDF will be saved
        public string UserPassword { get; set; }   // Password required to open the PDF
        public string OwnerPassword { get; set; }  // Password required to change permissions
    }

    class Program
    {
        static void Main()
        {
            const string configFile = "encryptionConfig.json"; // JSON file with an array of EncryptionTask objects
            const string logFile = "encryptionLog.txt";

            // Prepare a simple log writer
            using StreamWriter logWriter = new StreamWriter(logFile, append: true);
            void Log(string message)
            {
                string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                Console.WriteLine(entry);
                logWriter.WriteLine(entry);
            }

            // Validate configuration file existence
            if (!File.Exists(configFile))
            {
                Log($"Configuration file not found: {configFile}");
                return;
            }

            // Deserialize configuration
            List<EncryptionTask> tasks;
            try
            {
                string json = File.ReadAllText(configFile);
                tasks = JsonSerializer.Deserialize<List<EncryptionTask>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (tasks == null || tasks.Count == 0)
                {
                    Log("Configuration file is empty or malformed.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Log($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Define common permissions for all PDFs
            Permissions commonPermissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Process each task
            foreach (var task in tasks)
            {
                try
                {
                    // Verify input file
                    if (!File.Exists(task.InputPath))
                    {
                        Log($"Input file not found: {task.InputPath}");
                        continue;
                    }

                    // Ensure output directory exists
                    string outputDir = Path.GetDirectoryName(task.OutputPath);
                    if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    // Load, encrypt, and save the PDF using Aspose.Pdf lifecycle rules
                    using (Document doc = new Document(task.InputPath))
                    {
                        doc.Encrypt(
                            userPassword: task.UserPassword,
                            ownerPassword: task.OwnerPassword,
                            permissions: commonPermissions,
                            cryptoAlgorithm: CryptoAlgorithm.AESx256);

                        doc.Save(task.OutputPath);
                    }

                    Log($"Successfully encrypted '{task.InputPath}' -> '{task.OutputPath}'.");
                }
                catch (Exception ex)
                {
                    Log($"Error processing '{task.InputPath}': {ex.Message}");
                }
            }

            Log("Batch encryption completed.");
        }
    }
}