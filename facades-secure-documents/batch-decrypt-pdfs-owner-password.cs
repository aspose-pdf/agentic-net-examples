using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchDecrypt
{
    static void Main()
    {
        // Path to the configuration file.
        // Each line should contain: <inputPdfPath>|<ownerPassword>
        const string configPath = "decrypt_config.txt";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Process each line in the configuration file.
        foreach (string line in File.ReadLines(configPath))
        {
            // Skip empty or comment lines.
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            // Expected format: inputPath|ownerPassword
            string[] parts = line.Split(new[] { '|' }, 2);
            if (parts.Length != 2)
            {
                Console.Error.WriteLine($"Invalid config line (expected 'inputPath|ownerPassword'): {line}");
                continue;
            }

            string inputPath = parts[0].Trim();
            string ownerPassword = parts[1].Trim();

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                continue;
            }

            // Build output path by inserting "_decrypted" before the extension.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_decrypted.pdf");

            try
            {
                // PdfFileSecurity handles both decryption and saving.
                // It does not require a separate Document load/save.
                PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);
                bool result = fileSecurity.DecryptFile(ownerPassword);

                if (result)
                {
                    Console.WriteLine($"Successfully decrypted: '{inputPath}' → '{outputPath}'");
                }
                else
                {
                    Console.Error.WriteLine($"Decryption failed for: {inputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}