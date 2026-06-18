using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class BatchEncryptPdf
{
    // Compute a SHA256 hash of the input string and return a hex representation.
    private static string ComputeHash(string input)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    // Generate a password from file name and current date.
    private static string GeneratePassword(string filePath)
    {
        string namePart = Path.GetFileNameWithoutExtension(filePath);
        string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        string raw = $"{namePart}_{datePart}";
        // Use the full hash as password (or a subset if desired).
        return ComputeHash(raw);
    }

    static void Main()
    {
        // Directory containing PDFs to encrypt.
        const string inputDirectory = @"C:\PdfInput";
        // Directory where encrypted PDFs will be saved.
        const string outputDirectory = @"C:\PdfEncrypted";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Permissions to apply after encryption.
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                string password = GeneratePassword(pdfPath);
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(pdfPath));

                // Load, encrypt, and save the document.
                using (Document doc = new Document(pdfPath))
                {
                    doc.Encrypt(
                        userPassword: password,
                        ownerPassword: password,
                        permissions: perms,
                        cryptoAlgorithm: CryptoAlgorithm.AESx256);

                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted '{pdfPath}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}