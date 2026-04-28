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
        // Path to the JSON configuration file.
        private const string ConfigFilePath = "encryption_config.json";

        static void Main()
        {
            // Load and parse the configuration file.
            List<EncryptionJob> jobs = LoadConfiguration(ConfigFilePath);
            if (jobs == null || jobs.Count == 0)
            {
                Console.Error.WriteLine("No encryption jobs found in the configuration.");
                return;
            }

            // Process each job.
            foreach (EncryptionJob job in jobs)
            {
                try
                {
                    // Validate input file existence.
                    if (!File.Exists(job.InputPath))
                    {
                        Console.Error.WriteLine($"Input file not found: {job.InputPath}");
                        continue;
                    }

                    // Ensure the output directory exists.
                    string outputDir = Path.GetDirectoryName(job.OutputPath);
                    if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    // Open the PDF, encrypt it, and save the result.
                    using (Document doc = new Document(job.InputPath))
                    {
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                        doc.Encrypt(
                            userPassword: job.UserPassword,
                            ownerPassword: job.OwnerPassword,
                            permissions: perms,
                            cryptoAlgorithm: CryptoAlgorithm.AESx256);

                        doc.Save(job.OutputPath);
                    }

                    Console.WriteLine($"Encrypted '{job.InputPath}' -> '{job.OutputPath}'");
                }
                catch (Exception ex)
                {
                    // Log any errors for this job.
                    Console.Error.WriteLine($"Error processing '{job.InputPath}': {ex.Message}");
                }
            }
        }

        // Reads the JSON configuration file and deserializes it into a list of EncryptionJob objects.
        private static List<EncryptionJob> LoadConfiguration(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<EncryptionJob>>(json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load configuration file '{path}': {ex.Message}");
                return null;
            }
        }
    }
}