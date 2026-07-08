using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to protect
        const string secureFolder = "secure";

        // Identical user password for all PDFs
        const string userPassword = "MyUserPass";

        if (!Directory.Exists(secureFolder))
        {
            Console.Error.WriteLine($"Folder not found: {secureFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(secureFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (original name + "_protected")
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(secureFolder, $"{fileNameWithoutExt}_protected.pdf");

            // PdfFileSecurity loads the input PDF and saves the encrypted output
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Encrypt with the user password, no explicit owner password (null → random),
                // allow printing, and use 256‑bit AES encryption.
                bool encrypted = fileSecurity.EncryptFile(
                    userPassword,          // user password
                    null,                  // owner password (null → random)
                    DocumentPrivilege.Print, // set desired privilege
                    KeySize.x256);         // 256‑bit key size

                if (encrypted)
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