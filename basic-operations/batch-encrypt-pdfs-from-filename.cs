using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class BatchEncryptPdf
{
    static void Main()
    {
        // Input folder containing PDFs to encrypt
        const string inputFolder = @"C:\PdfInput";
        // Output folder for encrypted PDFs
        const string outputFolder = @"C:\PdfEncrypted";
        // Path to the password log file (store securely in production)
        const string logFilePath = @"C:\PdfEncrypted\passwords.log";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the log file for appending passwords
        using (StreamWriter logWriter = new StreamWriter(logFilePath, append: true, Encoding.UTF8))
        {
            // Process each PDF file in the input folder
            foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                try
                {
                    // Generate a unique password from the file name (without extension)
                    string fileName = Path.GetFileNameWithoutExtension(inputPath);
                    string password = GeneratePasswordFromName(fileName);

                    // Define output path (same file name in the output folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                    // Load the PDF document
                    using (Document doc = new Document(inputPath))
                    {
                        // Set desired permissions (example: allow printing and content extraction)
                        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                        // Encrypt using user and owner passwords (both the same here) and AES-256
                        doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

                        // Save the encrypted PDF
                        doc.Save(outputPath);
                    }

                    // Write the file name and its password to the log
                    logWriter.WriteLine($"{Path.GetFileName(inputPath)}\t{password}");
                }
                catch (Exception ex)
                {
                    // Log any errors (in a real scenario, consider a separate error log)
                    Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Batch encryption completed.");
    }

    // Generates a deterministic password from a file name using SHA-256.
    // Returns the first 16 characters of the hex string for brevity.
    private static string GeneratePasswordFromName(string name)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            // Use first 16 characters (8 bytes) as the password
            return sb.ToString().Substring(0, 16);
        }
    }
}