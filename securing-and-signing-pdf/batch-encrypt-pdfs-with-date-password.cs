using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class BatchPdfEncryptor
{
    // Generates a password based on the file name and the current UTC date (yyyyMMdd).
    // Uses SHA256 and returns the first 16 characters of the hex representation.
    private static string GeneratePassword(string fileName)
    {
        string input = $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.UtcNow:yyyyMMdd}";
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Convert to hex string
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            // Use first 16 characters (8 bytes) as the password
            return sb.ToString().Substring(0, 16);
        }
    }

    static void Main()
    {
        // Directory containing PDFs to encrypt
        const string inputDirectory = @"C:\PdfInput";
        // Directory where encrypted PDFs will be saved
        const string outputDirectory = @"C:\PdfEncrypted";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Define permissions for the encrypted PDFs (example: allow printing and content extraction)
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Generate a password based on file name and current date
                string password = GeneratePassword(pdfPath);
                string userPassword = password;   // password for opening the document
                string ownerPassword = password;  // password for changing permissions

                // Determine output file path (same name with "_enc" suffix)
                string outputPath = Path.Combine(
                    outputDirectory,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_enc.pdf");

                // Open, encrypt, and save the PDF using proper disposal pattern
                using (Document doc = new Document(pdfPath))
                {
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted '{Path.GetFileName(pdfPath)}' -> '{Path.GetFileName(outputPath)}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }
    }
}