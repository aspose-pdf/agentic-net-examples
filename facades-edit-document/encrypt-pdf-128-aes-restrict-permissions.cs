using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Use a privilege that forbids all actions (no printing, no editing)
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;

        // Encrypt using 128‑bit AES (KeySize.x128)
        bool encrypted = fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x128);

        if (!encrypted)
        {
            Console.Error.WriteLine("Encryption failed.");
        }
        else
        {
            Console.WriteLine($"PDF encrypted and saved to '{outputPath}'.");
        }
    }
}