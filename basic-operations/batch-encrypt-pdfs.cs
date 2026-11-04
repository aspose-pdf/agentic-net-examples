using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

namespace BatchPdfEncryption
{
    // Represents a single encryption job read from the configuration file
    public class EncryptionJob
    {
        public string InputPath { get; set; }      // Path to the source PDF
        public string OutputPath { get; set; }     // Path where the encrypted PDF will be saved
        public string UserPassword { get; set; }   // Password required to open the PDF
        public string OwnerPassword { get; set; }  // Password that grants full permissions
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

            string configPath = args[0];
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Load and deserialize the configuration file
            List<EncryptionJob> jobs;
            try
            {
                string json = File.ReadAllText(configPath);
                jobs = JsonSerializer.Deserialize<List<EncryptionJob>>(json);
                if (jobs == null)
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

            // Prepare a simple log file
            string logFile = Path.Combine(Path.GetDirectoryName(configPath) ?? "", "encryption_log.txt");
            using (StreamWriter logWriter = new StreamWriter(logFile, append: true))
            {
                foreach (var job in jobs)
                {
                    // Validate input PDF existence
                    if (!File.Exists(job.InputPath))
                    {
                        string msg = $"[ERROR] Input file not found: {job.InputPath}";
                        Console.Error.WriteLine(msg);
                        logWriter.WriteLine($"{DateTime.Now:u} {msg}");
                        continue;
                    }

                    try
                    {
                        // Load the source PDF inside a using block for deterministic disposal
                        using (Document doc = new Document(job.InputPath))
                        {
                            // Define permissions (adjust as needed)
                            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                            // Encrypt using the recommended AES-256 algorithm
                            doc.Encrypt(
                                userPassword: job.UserPassword,
                                ownerPassword: job.OwnerPassword,
                                permissions: perms,
                                cryptoAlgorithm: CryptoAlgorithm.AESx256);

                            // Save the encrypted PDF
                            doc.Save(job.OutputPath);
                        }

                        string successMsg = $"[SUCCESS] Encrypted '{job.InputPath}' -> '{job.OutputPath}'";
                        Console.WriteLine(successMsg);
                        logWriter.WriteLine($"{DateTime.Now:u} {successMsg}");
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = $"[ERROR] Failed to encrypt '{job.InputPath}': {ex.Message}";
                        Console.Error.WriteLine(errorMsg);
                        logWriter.WriteLine($"{DateTime.Now:u} {errorMsg}");
                    }
                }
            }

            Console.WriteLine($"Encryption process completed. Log written to: {logFile}");
        }
    }
}