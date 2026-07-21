using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string configPath = "decryptConfig.json";

        // Verify configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Load decryption instructions from JSON
        List<DecryptItem> items;
        try
        {
            string json = File.ReadAllText(configPath);
            items = JsonSerializer.Deserialize<List<DecryptItem>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // Process each file
        foreach (var item in items)
        {
            if (!File.Exists(item.Input))
            {
                Console.Error.WriteLine($"Input file not found: {item.Input}");
                continue;
            }

            try
            {
                // PdfFileSecurity handles decryption; it implements IDisposable
                using (PdfFileSecurity security = new PdfFileSecurity(item.Input, item.Output))
                {
                    bool success = security.DecryptFile(item.OwnerPassword);
                    Console.WriteLine(success
                        ? $"Decrypted: {item.Input} -> {item.Output}"
                        : $"Failed to decrypt: {item.Input}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {item.Input}: {ex.Message}");
            }
        }
    }

    // Helper class that matches the JSON structure:
    // [{ "Input": "path/to/input.pdf", "Output": "path/to/output.pdf", "OwnerPassword": "ownerPass" }, ...]
    private class DecryptItem
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public string OwnerPassword { get; set; }
    }
}