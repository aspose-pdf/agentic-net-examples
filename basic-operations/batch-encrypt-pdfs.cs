using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Configuration file format:
    // Each line: <pdfPath>|<userPassword>
    // Example:
    // C:\Docs\file1.pdf|userPass1
    // C:\Docs\file2.pdf|userPass2
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
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("|"))
                continue; // skip malformed lines

            string[] parts = line.Split(new[] { '|' }, 2);
            string pdfPath = parts[0].Trim();
            string userPassword = parts[1].Trim();
            string ownerPassword = "owner123"; // fixed owner password; adjust as needed

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            try
            {
                // Load the PDF, encrypt it, and save the encrypted version
                using (Document doc = new Document(pdfPath))
                {
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    string encryptedPath = Path.Combine(
                        Path.GetDirectoryName(pdfPath) ?? "",
                        Path.GetFileNameWithoutExtension(pdfPath) + "_encrypted.pdf");

                    doc.Save(encryptedPath);
                    Console.WriteLine($"Encrypted: '{pdfPath}' -> '{encryptedPath}' (UserPassword: '{userPassword}')");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}