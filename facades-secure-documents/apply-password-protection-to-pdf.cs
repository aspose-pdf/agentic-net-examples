using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt the PDF:
        // - userPassword / ownerPassword set as provided
        // - DocumentPrivilege.Print allows printing only
        // - KeySize.x256 selects 256‑bit AES encryption
        bool encrypted = fileSecurity.EncryptFile(userPassword, ownerPassword,
                                                  DocumentPrivilege.Print, KeySize.x256);

        if (!encrypted)
        {
            Console.Error.WriteLine($"Encryption failed: {fileSecurity.LastException?.Message}");
            return;
        }

        // EncryptFile writes the output file; no further Save call is required
        Console.WriteLine($"PDF encrypted successfully: {outputPath}");
    }
}