using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

namespace BatchPdfEncryption
{
    // Simple POCO to represent each entry in the configuration file
    public class EncryptionEntry
    {
        public string InputPath { get; set; }
        public string UserPassword { get; set; }
    }

    class Program
    {
        // Owner password used for all PDFs (can be changed as needed)
        private const string OwnerPassword = "owner123";

        // Permissions granted after encryption (adjust as required)
        private static readonly Permissions AllowedPermissions =
            Permissions.PrintDocument | Permissions.ExtractContent;

        static void Main()
        {
            const string configFile = "encryptionConfig.json";   // JSON config file path
            const string outputFolder = "Encrypted";            // Folder for encrypted PDFs
            const string logFile = "encryptionLog.txt";        // Simple log file

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            // Prepare log writer
            using StreamWriter logWriter = new StreamWriter(logFile, append: true);

            // Load configuration
            List<EncryptionEntry> entries;
            try
            {
                string json = File.ReadAllText(configFile);
                entries = JsonSerializer.Deserialize<List<EncryptionEntry>>(json);
                if (entries == null)
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

            // Process each PDF
            foreach (var entry in entries)
            {
                string inputPath = entry.InputPath;
                string userPassword = entry.UserPassword;

                if (!File.Exists(inputPath))
                {
                    string msg = $"File not found: {inputPath}";
                    Console.Error.WriteLine(msg);
                    logWriter.WriteLine($"{DateTime.Now:u} - ERROR - {msg}");
                    continue;
                }

                // Determine output path (same file name with .enc.pdf suffix)
                string outputPath = Path.Combine(
                    outputFolder,
                    Path.GetFileNameWithoutExtension(inputPath) + ".enc.pdf");

                try
                {
                    // Load the PDF, encrypt, and save
                    using (Document doc = new Document(inputPath))
                    {
                        doc.Encrypt(userPassword, OwnerPassword, AllowedPermissions, CryptoAlgorithm.AESx256);
                        doc.Save(outputPath);
                    }

                    string successMsg = $"Encrypted '{inputPath}' -> '{outputPath}'";
                    Console.WriteLine(successMsg);
                    logWriter.WriteLine($"{DateTime.Now:u} - SUCCESS - {successMsg}");
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Failed to encrypt '{inputPath}': {ex.Message}";
                    Console.Error.WriteLine(errorMsg);
                    logWriter.WriteLine($"{DateTime.Now:u} - ERROR - {errorMsg}");
                }
            }

            Console.WriteLine("Batch encryption completed. See log for details.");
        }
    }
}