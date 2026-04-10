using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileSecurity, DocumentPrivilege, KeySize, Algorithm

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

        // Combine privileges: allow printing and editing (modify contents & annotations)
        DocumentPrivilege privilege = DocumentPrivilege.Print;
        privilege.AllowModifyContents = true;
        privilege.AllowModifyAnnotations = true;

        // Initialize PdfFileSecurity with source and destination files
        PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using RC4 with 128‑bit key
        bool result = security.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x128, Algorithm.RC4);

        if (result)
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine($"Encryption failed: {security.LastException?.Message}");
    }
}