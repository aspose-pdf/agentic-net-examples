using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination file names
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using only a user password.
        // Owner password is passed as null – a random owner password will be generated.
        // DocumentPrivilege.Print allows printing; adjust as needed.
        // KeySize.x256 selects 256‑bit AES encryption.
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,          // user password
            null,                  // owner password (undefined)
            DocumentPrivilege.Print,
            KeySize.x256);

        if (encrypted)
        {
            Console.WriteLine($"PDF encrypted successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Encryption failed.");
        }

        // PdfFileSecurity writes the output file during EncryptFile; no further Save call is required.
    }
}