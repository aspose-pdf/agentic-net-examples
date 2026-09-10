using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define custom privileges: allow printing and copying, forbid modifications
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowModifyContents = false;
        privilege.AllowModifyAnnotations = false;

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using AES‑256 (KeySize.x256) and the custom privilege
        bool encrypted = fileSecurity.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x256, Algorithm.AES);

        if (encrypted)
        {
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Encryption failed.");
        }
    }
}