using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege enum
using Aspose.Pdf.Facades;      // PdfFileSecurity, KeySize, Algorithm

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_rc4_40.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = ""; // empty => random owner password will be generated

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // Encrypt using RC4‑40 algorithm; allow printing as an example privilege
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x40,
                Algorithm.RC4);

            if (!encrypted)
            {
                Console.Error.WriteLine("Encryption failed.");
                if (security.LastException != null)
                    Console.Error.WriteLine(security.LastException.Message);
            }
            else
            {
                Console.WriteLine($"File encrypted successfully: {outputPath}");
            }
        }
    }
}