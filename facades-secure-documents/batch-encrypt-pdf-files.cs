using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for DocumentPrivilege and KeySize enums

class Program
{
    static void Main()
    {
        // Directory containing PDF files to encrypt
        const string folderPath = @"C:\PdfFolder";

        // Passwords to apply to all PDFs
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (e.g., original_encrypted.pdf)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(folderPath, $"{fileName}_encrypted.pdf");

            // Initialize PdfFileSecurity with input and output file paths
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Encrypt: allow printing, use 256‑bit AES encryption
                bool success = security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256);

                if (success)
                {
                    Console.WriteLine($"Encrypted: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to encrypt: {inputPath}");
                }
            }
        }
    }
}