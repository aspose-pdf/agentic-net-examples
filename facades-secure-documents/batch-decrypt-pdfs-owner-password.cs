using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchDecrypt
{
    static void Main()
    {
        // Path to the configuration file.
        // Expected format per line: <inputPdfPath>,<outputPdfPath>,<ownerPassword>
        const string configPath = "decrypt_config.txt";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Process each line in the configuration file.
        foreach (string line in File.ReadLines(configPath))
        {
            // Skip empty lines or comment lines.
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            // Split the line into parts.
            string[] parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                Console.Error.WriteLine($"Invalid config entry (expected 3 comma‑separated values): {line}");
                continue;
            }

            string inputPdf  = parts[0].Trim();
            string outputPdf = parts[1].Trim();
            string ownerPwd  = parts[2].Trim();

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                continue;
            }

            try
            {
                // PdfFileSecurity handles decryption without loading the whole document into memory.
                // The constructor takes the source and destination file paths.
                using (PdfFileSecurity security = new PdfFileSecurity(inputPdf, outputPdf))
                {
                    bool result = security.DecryptFile(ownerPwd);
                    if (result)
                    {
                        Console.WriteLine($"Successfully decrypted: {inputPdf} → {outputPdf}");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Decryption failed for: {inputPdf}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPdf}': {ex.Message}");
            }
        }
    }
}