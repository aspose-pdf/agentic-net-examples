using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf; // Document, Permissions, CryptoAlgorithm

class BatchPdfEncryptor
{
    // Generates a password by hashing the file name with the current date.
    private static string GeneratePassword(string fileName)
    {
        // Combine file name (without extension) and current date (yyyyMMdd).
        string input = Path.GetFileNameWithoutExtension(fileName) + DateTime.UtcNow.ToString("yyyyMMdd");
        // Compute SHA256 hash.
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Convert to a Base64 string and take a reasonable length for a password.
            return Convert.ToBase64String(hashBytes).Substring(0, 16);
        }
    }

    static void Main(string[] args)
    {
        // Use directories relative to the executable so the code works on any OS.
        string baseDir = AppContext.BaseDirectory;
        string inputDirectory = Path.Combine(baseDir, "PdfInput");
        string outputDirectory = Path.Combine(baseDir, "PdfEncrypted");

        // Ensure both directories exist (create if missing).
        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(outputDirectory);

        // Define permissions for the encrypted PDFs.
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Process each PDF file in the input directory.
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileName);

            // Generate a password based on file name and current date.
            string password = GeneratePassword(fileName);

            try
            {
                // Load the PDF document.
                using (Document doc = new Document(inputPath))
                {
                    // Encrypt using the generated password for both user and owner.
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);
                    // Save the encrypted PDF.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted '{fileName}' -> '{outputPath}' with password '{password}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}
