using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a custom privilege set: allow printing, forbid copying and modifying contents
            DocumentPrivilege privilege = DocumentPrivilege.ForbidAll;
            privilege.AllowPrint = true;
            privilege.AllowCopy = false;
            privilege.AllowModifyContents = false;

            // Initialize PdfFileSecurity with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Encrypt using AES‑256 (KeySize.x256 + Algorithm.AES) and the custom privileges in one call
            bool success = fileSecurity.EncryptFile(
                userPassword,
                ownerPassword,
                privilege,
                KeySize.x256,
                Algorithm.AES);

            if (success)
                Console.WriteLine($"File encrypted successfully to '{outputPath}'.");
            else
                Console.Error.WriteLine("Encryption failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}