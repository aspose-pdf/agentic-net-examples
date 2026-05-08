using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades; // PdfFileSecurity resides here

// Define a simple POCO that matches the JSON configuration structure.
public class DecryptConfig
{
    public List<FileEntry> Files { get; set; }
}

public class FileEntry
{
    public string InputPath { get; set; }      // Path to the encrypted PDF
    public string OutputPath { get; set; }     // Desired path for the decrypted PDF
    public string OwnerPassword { get; set; }  // Owner password for decryption
}

class Program
{
    static void Main()
    {
        const string configPath = "decryptConfig.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load and deserialize the configuration file.
        DecryptConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<DecryptConfig>(json);
            if (config?.Files == null || config.Files.Count == 0)
            {
                Console.Error.WriteLine("No files defined in the configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Process each file entry.
        foreach (FileEntry entry in config.Files)
        {
            // Validate paths.
            if (string.IsNullOrWhiteSpace(entry.InputPath) ||
                string.IsNullOrWhiteSpace(entry.OutputPath) ||
                string.IsNullOrWhiteSpace(entry.OwnerPassword))
            {
                Console.Error.WriteLine("Invalid entry detected; skipping.");
                continue;
            }

            if (!File.Exists(entry.InputPath))
            {
                Console.Error.WriteLine($"Input file not found: {entry.InputPath}");
                continue;
            }

            try
            {
                // PdfFileSecurity constructor takes input and output file paths.
                // It implements IDisposable, so we wrap it in a using block.
                using (PdfFileSecurity security = new PdfFileSecurity(entry.InputPath, entry.OutputPath))
                {
                    // DecryptFile returns true on success; false otherwise.
                    bool success = security.DecryptFile(entry.OwnerPassword);
                    if (success)
                    {
                        Console.WriteLine($"Decrypted: '{entry.InputPath}' → '{entry.OutputPath}'");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to decrypt: {entry.InputPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{entry.InputPath}': {ex.Message}");
            }
        }
    }
}