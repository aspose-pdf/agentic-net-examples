using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = ""; // optional owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the security facade with source and destination files
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Encrypt using RC4‑40 (KeySize.x40). DocumentPrivilege can be set as needed.
                bool encrypted = security.EncryptFile(userPassword, ownerPassword,
                    DocumentPrivilege.Print, KeySize.x40);

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
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}