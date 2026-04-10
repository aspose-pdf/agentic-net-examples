using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Define custom privileges (modify as needed)
        // ------------------------------------------------------------
        DocumentPrivilege privilege = DocumentPrivilege.ForbidAll; // start with all disabled
        privilege.AllowPrint           = true;   // enable printing
        privilege.AllowCopy            = false;  // disable copying
        privilege.AllowModifyContents = false;  // disable content modification
        privilege.AllowFillIn          = true;   // enable form filling

        // ------------------------------------------------------------
        // Initialize the facade with source and destination files
        // ------------------------------------------------------------
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // ------------------------------------------------------------
        // Encrypt using AES‑256 and the custom privileges in a single step
        // ------------------------------------------------------------
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            privilege,
            KeySize.x256,   // 256‑bit key size
            Algorithm.AES   // AES algorithm
        );

        // ------------------------------------------------------------
        // Report result
        // ------------------------------------------------------------
        if (encrypted)
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Encryption failed.");
    }
}