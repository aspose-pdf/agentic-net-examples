using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf; // CryptoAlgorithm and Permissions are in this namespace

namespace BatchPdfEncryption
{
    // Represents a single encryption job read from the configuration file
    public class EncryptionJob
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
            const string configFile = "encryption_config.json"; // JSON file with an array of EncryptionJob objects
            const string logFile = "encryption_log.txt";

            // Prepare the log file (append mode)
            using (StreamWriter logWriter = new StreamWriter(logFile, append: true))
            {
                // Validate configuration file existence
                if (!File.Exists(configFile))
                {
                    string msg = $"Configuration file not found: {configFile}";
                    Console.Error.WriteLine(msg);
                    logWriter.WriteLine($"{DateTime.Now:u} ERROR: {msg}");
                    return;
                }

                // Read and deserialize the configuration
                List<EncryptionJob> jobs;
                try
                {
                    string json = File.ReadAllText(configFile);
                    jobs = JsonSerializer.Deserialize<List<EncryptionJob>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (jobs == null)
                        throw new InvalidOperationException("Deserialized job list is null.");
                }
                catch (Exception ex)
                {
                    string msg = $"Failed to read configuration: {ex.Message}";
                    Console.Error.WriteLine(msg);
                    logWriter.WriteLine($"{DateTime.Now:u} ERROR: {msg}");
                    return;
                }

                // Process each encryption job
                foreach (EncryptionJob job in jobs)
                {
                    try
                    {
                        // Verify source PDF exists
                        if (!File.Exists(job.InputPath))
                            throw new FileNotFoundException("Source PDF not found.", job.InputPath);

                        // Ensure output directory exists
                        string outputDir = Path.GetDirectoryName(job.OutputPath);
                        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                            Directory.CreateDirectory(outputDir);

                        // Define desired permissions (example: allow printing and content extraction)
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                        // Open, encrypt, and save the PDF using proper using blocks for deterministic disposal
                        using (Document doc = new Document(job.InputPath))
                        {
                            doc.Encrypt(
                                userPassword: job.UserPassword,
                                ownerPassword: job.OwnerPassword,
                                permissions: perms,
                                cryptoAlgorithm: CryptoAlgorithm.AESx256);

                            doc.Save(job.OutputPath); // Saves as PDF; extension determines format
                        }

                        string successMsg = $"Encrypted '{job.InputPath}' -> '{job.OutputPath}'";
                        Console.WriteLine(successMsg);
                        logWriter.WriteLine($"{DateTime.Now:u} SUCCESS: {successMsg}");
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = $"Failed to encrypt '{job.InputPath}': {ex.Message}";
                        Console.Error.WriteLine(errorMsg);
                        logWriter.WriteLine($"{DateTime.Now:u} ERROR: {errorMsg}");
                    }
                }
            }
        }
    }
}