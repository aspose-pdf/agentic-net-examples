using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege, KeySize enums
using Aspose.Pdf.Facades;      // PdfFileSecurity

class Program
{
    static void Main()
    {
        // Directory containing the PDFs to encrypt
        const string inputDirectory  = @"C:\PdfInput";
        // Directory where encrypted PDFs will be saved
        const string outputDirectory = @"C:\PdfEncrypted";

        // User and owner passwords to apply to every file
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify input directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string inputPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileName);

            // Initialize PdfFileSecurity with source and destination files
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Encrypt the file using AES‑256 (KeySize.x256) and allow printing
                fileSecurity.EncryptFile(userPassword, ownerPassword,
                                         DocumentPrivilege.Print, KeySize.x256);
            }

            Console.WriteLine($"Encrypted: {fileName}");
        }

        Console.WriteLine("Batch encryption completed.");
    }
}