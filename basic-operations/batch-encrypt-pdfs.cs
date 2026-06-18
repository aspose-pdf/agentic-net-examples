using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

namespace BatchPdfEncryption
{
    // Represents a single encryption job read from the configuration file.
    public class EncryptionJob
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public string UserPassword { get; set; }
        public string OwnerPassword { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path of the JSON configuration file.
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

            List<EncryptionJob> jobs;
            try
            {
                string json = File.ReadAllText(configFile);
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

            foreach (var job in jobs)
            {
                // Basic validation of paths.
                if (string.IsNullOrWhiteSpace(job.InputPath) ||
                    string.IsNullOrWhiteSpace(job.OutputPath) ||
                    string.IsNullOrWhiteSpace(job.UserPassword) ||
                    string.IsNullOrWhiteSpace(job.OwnerPassword))
                {
                    Console.Error.WriteLine("Invalid job definition – missing required fields.");
                    continue;
                }

                if (!File.Exists(job.InputPath))
                {
                    Console.Error.WriteLine($"Input PDF not found: {job.InputPath}");
                    continue;
                }

                try
                {
                    // Load the PDF, encrypt it, and save the result.
                    using (Document doc = new Document(job.InputPath))
                    {
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                        doc.Encrypt(job.UserPassword, job.OwnerPassword, perms, CryptoAlgorithm.AESx256);
                        doc.Save(job.OutputPath);
                    }

                    Console.WriteLine($"SUCCESS: Encrypted '{job.InputPath}' -> '{job.OutputPath}'");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"FAILURE: Could not encrypt '{job.InputPath}'. Error: {ex.Message}");
                }
            }
        }
    }
}