using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSecurity, DocumentPrivilege, KeySize, Algorithm

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Combine privileges: allow printing and editing (modify contents)
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
        privilege.AllowPrint          = true; // enable printing
        privilege.AllowModifyContents = true; // enable editing of content

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using RC4 with a 128‑bit key
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            privilege,
            KeySize.x128,
            Algorithm.RC4);

        if (encrypted)
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Encryption failed.");
    }
}