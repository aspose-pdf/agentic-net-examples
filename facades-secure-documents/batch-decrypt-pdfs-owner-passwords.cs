using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string configPath = "decrypt_config.txt";

        // Verify configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load input PDF paths and their corresponding owner passwords
        Dictionary<string, string> pdfEntries = LoadConfig(configPath);

        foreach (KeyValuePair<string, string> entry in pdfEntries)
        {
            string inputPdf = entry.Key;
            string ownerPassword = entry.Value;

            // Skip missing source files
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
                continue;
            }

            // Determine output file name (e.g., MyDoc_decrypted.pdf)
            string outputPdf = GetDecryptedPath(inputPdf);

            try
            {
                // PdfFileSecurity handles opening, decrypting, and saving the file.
                using (PdfFileSecurity security = new PdfFileSecurity(inputPdf, outputPdf))
                {
                    bool decrypted = security.DecryptFile(ownerPassword);
                    if (decrypted)
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

    // Reads a simple configuration file where each line is:
    // <inputPdfPath>|<ownerPassword>
    // Lines starting with '#' are treated as comments.
    static Dictionary<string, string> LoadConfig(string configFile)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string rawLine in File.ReadAllLines(configFile))
        {
            string line = rawLine.Trim();
            if (line.Length == 0 || line.StartsWith("#"))
                continue;

            string[] parts = line.Split(new[] { '|' }, 2);
            if (parts.Length == 2)
            {
                string pdfPath = parts[0].Trim();
                string password = parts[1].Trim();
                map[pdfPath] = password;
            }
        }
        return map;
    }

    // Generates an output file name by inserting "_decrypted" before the extension.
    static string GetDecryptedPath(string inputPath)
    {
        string directory = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string extension = Path.GetExtension(inputPath);
        return Path.Combine(directory, $"{fileNameWithoutExt}_decrypted{extension}");
    }
}