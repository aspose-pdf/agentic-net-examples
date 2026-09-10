using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege enum
using Aspose.Pdf.Facades;      // PdfFileSecurity, KeySize

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the security facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using 256‑bit AES; allow printing as an example privilege
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256);

        if (!encrypted)
        {
            Console.Error.WriteLine("Encryption failed.");
        }
        else
        {
            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
    }
}