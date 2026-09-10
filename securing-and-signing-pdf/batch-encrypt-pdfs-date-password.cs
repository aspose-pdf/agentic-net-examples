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

        // Use UTC date in yyyyMMdd format as part of the password seed
        string datePart = DateTime.UtcNow.ToString("yyyyMMdd");

        // Process each PDF file in the input directory
        foreach (string filePath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(filePath);

            // Generate a password by hashing the file name combined with the date
            string password = ComputeHash(fileName + datePart);

            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(filePath))
                {
                    // Define desired permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using the same password for user and owner, AES-256 algorithm
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF (lifecycle rule: use Save inside using)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt {fileName}: {ex.Message}");
            }
        }
    }

    // Computes a SHA-256 hash of the input string and returns it as a hex string
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