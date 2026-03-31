using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string configPath = "config.txt";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        string[] lines = File.ReadAllLines(configPath);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split('|');
            if (parts.Length != 2)
            {
                Console.Error.WriteLine($"Invalid config line (expected 'inputPath|ownerPassword'): {line}");
                continue;
            }

            string inputFile = parts[0].Trim();
            string ownerPassword = parts[1].Trim();

            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputFile}");
                continue;
            }

            string outputFile = Path.GetFileNameWithoutExtension(inputFile) + "_decrypted.pdf";

            try
            {
                using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputFile, outputFile))
                {
                    bool success = fileSecurity.DecryptFile(ownerPassword);
                    if (success)
                    {
                        Console.WriteLine($"Decrypted: {inputFile} -> {outputFile}");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to decrypt: {inputFile}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {inputFile}: {ex.Message}");
            }
        }
    }
}
