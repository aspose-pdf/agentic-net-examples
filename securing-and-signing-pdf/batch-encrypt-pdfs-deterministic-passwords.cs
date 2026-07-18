using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class BatchEncrypt
{
    static void Main()
    {
        // Input folder containing PDFs to encrypt
        const string inputDir = "InputPdfs";
        // Output folder for encrypted PDFs
        const string outputDir = "EncryptedPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build a deterministic password from file name + current UTC date (yyyyMMdd)
                string baseName = Path.GetFileNameWithoutExtension(inputPath);
                string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
                string raw = baseName + datePart;

                // Compute SHA‑256 hash and use the first 16 characters as the password
                string password = ComputeHash(raw).Substring(0, 16);

                // Use the same password for user and owner (can be different if desired)
                string userPassword = password;
                string ownerPassword = password;

                // Define permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Load, encrypt, and save the document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));
                    doc.Save(outputPath); // Save writes a PDF regardless of extension
                }

                Console.WriteLine($"Encrypted: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }

    // Helper: compute SHA‑256 hash of a string and return it as a hex string
    static string ComputeHash(string input)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}