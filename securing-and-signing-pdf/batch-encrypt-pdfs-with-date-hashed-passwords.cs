using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class BatchPdfEncryptor
{
    // Generates a SHA256 hash string from the given input.
    private static string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    // Creates a password based on file name and current date (yyyyMMdd).
    private static string GeneratePassword(string fileName)
    {
        string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        string raw = $"{Path.GetFileNameWithoutExtension(fileName)}_{datePart}";
        // Use the full hash as password; you may truncate if desired.
        return ComputeHash(raw);
    }

    static void Main()
    {
        const string inputFolder  = @"C:\PdfInput";
        const string outputFolder = @"C:\PdfEncrypted";

        // Ensure output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder.
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Generate passwords.
                string userPassword  = GeneratePassword(inputPath);
                string ownerPassword = userPassword + "_owner";

                // Load the PDF document.
                using (Document doc = new Document(inputPath))
                {
                    // Define desired permissions (example: allow printing and content extraction).
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES-256 (preferred algorithm).
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Build output file path.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                    // Save the encrypted PDF.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputPath)}': {ex.Message}");
            }
        }
    }
}