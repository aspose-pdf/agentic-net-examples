using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class DecryptConfigEntry
{
    public required string InputPath { get; set; }
    public required string OutputPath { get; set; }
    public required string OwnerPassword { get; set; }
}

class Program
{
    static void Main()
    {
        const string configFile = "decrypt_config.json";

        if (!File.Exists(configFile))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFile}");
            return;
        }

        List<DecryptConfigEntry> entries;
        try
        {
            string json = File.ReadAllText(configFile);
            entries = JsonSerializer.Deserialize<List<DecryptConfigEntry>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        if (entries == null || entries.Count == 0)
        {
            Console.WriteLine("No decryption entries found in configuration.");
            return;
        }

        foreach (var entry in entries)
        {
            if (string.IsNullOrWhiteSpace(entry.InputPath) ||
                string.IsNullOrWhiteSpace(entry.OutputPath) ||
                string.IsNullOrWhiteSpace(entry.OwnerPassword))
            {
                Console.Error.WriteLine("Invalid entry detected; skipping.");
                continue;
            }

            if (!File.Exists(entry.InputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {entry.InputPath}");
                continue;
            }

            try
            {
                // Check whether the PDF is encrypted.
                var fileInfo = new PdfFileInfo(entry.InputPath);
                bool isEncrypted = fileInfo.IsEncrypted;

                if (!isEncrypted)
                {
                    // If the file is not encrypted, just copy it to the output location.
                    File.Copy(entry.InputPath, entry.OutputPath, true);
                    Console.WriteLine($"File not encrypted, copied: '{entry.InputPath}' → '{entry.OutputPath}'");
                }
                else
                {
                    // Decrypt the encrypted PDF using the owner password.
                    using var security = new PdfFileSecurity();
                    security.BindPdf(entry.InputPath);
                    security.DecryptFile(entry.OwnerPassword);
                    security.Save(entry.OutputPath);

                    Console.WriteLine($"Decrypted: '{entry.InputPath}' → '{entry.OutputPath}'");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{entry.InputPath}': {ex.Message}");
            }
        }
    }
}
