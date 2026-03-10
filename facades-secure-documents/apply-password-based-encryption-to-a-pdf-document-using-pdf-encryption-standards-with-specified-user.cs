using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege enum
using Aspose.Pdf.Facades;      // PdfFileSecurity, KeySize

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination file names
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Encrypt the PDF:
            // - userPassword / ownerPassword as supplied
            // - allow printing (DocumentPrivilege.Print)
            // - use 256‑bit key size (AES encryption)
            bool encrypted = fileSecurity.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256);

            if (encrypted)
                Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Encryption failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}