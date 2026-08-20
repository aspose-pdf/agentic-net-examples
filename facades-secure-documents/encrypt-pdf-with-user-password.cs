using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using only a user password; owner password is left undefined (null)
        // Example privilege: allow printing; use 256‑bit AES encryption
        bool result = fileSecurity.EncryptFile(userPassword, null, DocumentPrivilege.Print, KeySize.x256);

        if (result)
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Encryption failed.");
    }
}