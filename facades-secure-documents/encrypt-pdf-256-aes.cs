using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // Encrypt using 256‑bit AES. Choose any required privileges; here we allow printing.
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256,
                Algorithm.AES);

            if (!encrypted)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
    }
}