using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // User password to protect the PDF; owner password is left undefined (null)
        const string userPassword = "user123";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSecurity facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt the PDF using only the user password.
        // Owner password is null, so a random one will be generated internally.
        // DocumentPrivilege.Print grants printing permission; adjust as needed.
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
    }
}