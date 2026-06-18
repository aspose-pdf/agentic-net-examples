using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to encrypt
        const string inputDirectory = @"C:\PdfFiles";
        // Passwords to apply to every PDF
        const string userPassword  = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Enumerate all PDF files (case‑insensitive) in the directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to encrypt.");
            return;
        }

        foreach (string sourcePath in pdfFiles)
        {
            try
            {
                // Build output file name – original name with "_encrypted" suffix
                string fileName   = Path.GetFileNameWithoutExtension(sourcePath);
                string outputPath = Path.Combine(inputDirectory, $"{fileName}_encrypted.pdf");

                // Initialize the facade with source and destination files
                using (PdfFileSecurity security = new PdfFileSecurity(sourcePath, outputPath))
                {
                    // Encrypt using AES‑256 and allow printing (adjust privileges as needed)
                    bool success = security.EncryptFile(
                        userPassword,
                        ownerPassword,
                        DocumentPrivilege.Print,
                        KeySize.x256,
                        Algorithm.AES);

                    if (!success)
                    {
                        Console.Error.WriteLine($"Encryption failed for: {sourcePath}");
                    }
                }

                Console.WriteLine($"Encrypted: {sourcePath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }
}