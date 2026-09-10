using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Define privileges: forbid all actions (no printing, no editing, etc.)
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;

        // Encrypt using 128‑bit AES
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            privilege,
            KeySize.x128,
            Algorithm.AES);

        if (!encrypted)
        {
            Console.Error.WriteLine("Encryption failed.");
        }
        else
        {
            Console.WriteLine($"PDF encrypted successfully to '{outputPath}'.");
        }

        // Release resources
        fileSecurity.Close();
    }
}