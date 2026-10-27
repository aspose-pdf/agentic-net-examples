using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the configuration file.
        // Each line should contain: <pdfPath>|<ownerPassword>
        const string configPath = "decrypt_config.txt";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Process each entry in the configuration file.
        foreach (string rawLine in File.ReadLines(configPath))
        {
            // Skip empty lines or comments.
            if (string.IsNullOrWhiteSpace(rawLine) || rawLine.TrimStart().StartsWith("#"))
                continue;

            // Expected format: inputPdfPath|ownerPassword
            string[] parts = rawLine.Split('|');
            if (parts.Length != 2)
            {
                Console.Error.WriteLine($"Invalid line (expected 'path|password'): {rawLine}");
                continue;
            }

            string inputPdf = parts[0].Trim();
            string ownerPassword = parts[1].Trim();

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
                continue;
            }

            // Build output file name (original name with "_decrypted" suffix).
            string outputPdf = Path.Combine(
                Path.GetDirectoryName(inputPdf) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPdf) + "_decrypted.pdf");

            try
            {
                // Load the encrypted PDF using the owner password.
                Document pdfDoc = new Document(inputPdf, ownerPassword);
                // Saving without providing a password writes an unencrypted PDF.
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"Decrypted: {inputPdf} → {outputPdf}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to decrypt '{inputPdf}': {ex.Message}");
            }
        }
    }
}
