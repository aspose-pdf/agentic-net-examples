using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Permissions, CryptoAlgorithm

class BatchPdfEncryptor
{
    static void Main()
    {
        // Input folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfInput";
        // Output folder where encrypted PDFs will be saved
        const string outputFolder = @"C:\PdfEncrypted";
        // Path to the log file that will store file‑name → password mappings
        const string logFilePath = @"C:\PdfEncrypted\encryption_log.txt";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the log file for appending (creates it if it does not exist)
        using (StreamWriter logWriter = new StreamWriter(logFilePath, append: true))
        {
            // Process each PDF file in the input folder
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                // Derive a password from the file name (e.g., SHA‑256 hash, first 16 characters)
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string password = GeneratePasswordFromFileName(fileName);

                // Build the output file path (same name, different folder)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                // Encrypt the PDF using Aspose.Pdf
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions (adjust as needed)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt with the same password for user and owner; use AES‑256
                    doc.Encrypt(userPassword: password,
                                ownerPassword: password,
                                permissions: perms,
                                cryptoAlgorithm: CryptoAlgorithm.AESx256);

                    // Save the encrypted document
                    doc.Save(outputPath);
                }

                // Record the password in the log (format: FileName:Password)
                logWriter.WriteLine($"{fileName}:{password}");
                Console.WriteLine($"Encrypted '{fileName}.pdf' → '{outputPath}'");
            }
        }

        Console.WriteLine("Batch encryption completed. Passwords stored in log file.");
    }

    // Generates a deterministic password from a file name using SHA‑256.
    // Returns a 16‑character hexadecimal string (suitable for PDF passwords).
    private static string GeneratePasswordFromFileName(string fileName)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(fileName));
            // Convert first 8 bytes (16 hex chars) to a string
            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 8; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}