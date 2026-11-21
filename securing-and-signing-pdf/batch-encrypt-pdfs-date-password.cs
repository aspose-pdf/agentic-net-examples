using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Aspose.Pdf;

// Suppress NuGet vulnerability warnings for the transitive package System.Security.Cryptography.Pkcs.
// The current sample does not use any PKCS functionality, and the warning is treated as informational.
[assembly: SuppressMessage("NuGet", "NU1902", Justification = "Package is a transitive dependency and not used directly.")]
[assembly: SuppressMessage("NuGet", "NU1903", Justification = "Package is a transitive dependency and not used directly.")]

class BatchEncryptPdf
{
    static void Main()
    {
        // Base directory of the application (works cross‑platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input and output directories (relative to the base directory)
        string inputDir = Path.Combine(baseDir, "input_pdfs");
        string outputDir = Path.Combine(baseDir, "encrypted_pdfs");

        // Validate / create directories
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            try
            {
                // Generate a password based on file name and current date
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
                string hashInput = fileName + datePart;

                // Compute SHA256 hash and take first 16 characters as password
                string password = ComputeHashPassword(hashInput, 16);

                // Load the PDF, encrypt it, and save to the output folder
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES‑256 algorithm
                    doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                    // Save encrypted PDF
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Encrypted '{Path.GetFileName(pdfPath)}' with password '{password}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to process '{pdfPath}': {ex.Message}");
            }
        }
    }

    // Helper method to compute SHA256 hash and return a hex string of desired length
    private static string ComputeHashPassword(string input, int length)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            // Ensure the requested length does not exceed the hash length
            return sb.ToString().Substring(0, Math.Min(length, sb.Length));
        }
    }
}