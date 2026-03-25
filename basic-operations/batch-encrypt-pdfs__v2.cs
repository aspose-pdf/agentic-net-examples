using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        const string configPath = "encrypt_config.txt";
        const string logPath = "encryption_log.txt";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        var logLines = new List<string>();
        foreach (var rawLine in File.ReadAllLines(configPath))
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue; // skip empty or comment lines

            // Expected CSV format: inputPdfPath,userPassword,ownerPassword
            var parts = line.Split(',');
            if (parts.Length < 3)
            {
                logLines.Add($"Invalid config line (expected 3 fields): {line}");
                continue;
            }

            string inputPdf = parts[0].Trim();
            string userPwd = parts[1].Trim();
            string ownerPwd = parts[2].Trim();
            string outputPdf = Path.Combine(
                Path.GetDirectoryName(inputPdf) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdf) + "_enc.pdf");

            if (!File.Exists(inputPdf))
            {
                logLines.Add($"Source file not found: {inputPdf}");
                continue;
            }

            try
            {
                using (Document doc = new Document(inputPdf))
                {
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                    doc.Encrypt(userPwd, ownerPwd, perms, CryptoAlgorithm.AESx256);
                    doc.Save(outputPdf);
                }
                logLines.Add($"Encrypted: {inputPdf} -> {outputPdf}");
            }
            catch (Exception ex)
            {
                logLines.Add($"Error encrypting {inputPdf}: {ex.Message}");
            }
        }

        File.WriteAllLines(logPath, logLines);
        Console.WriteLine($"Batch encryption finished. Log written to '{logPath}'.");
    }
}