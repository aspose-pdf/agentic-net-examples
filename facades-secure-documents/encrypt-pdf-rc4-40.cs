using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege enum
using Aspose.Pdf.Facades;      // PdfFileSecurity, KeySize, Algorithm

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_rc4_40.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination file names
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using RC4‑40 algorithm.
        // DocumentPrivilege.Print is used as an example; adjust as needed.
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x40,
            Algorithm.RC4);

        if (encrypted)
            Console.WriteLine($"Encryption succeeded. Encrypted file: {outputPath}");
        else
            Console.Error.WriteLine("Encryption failed.");
    }
}