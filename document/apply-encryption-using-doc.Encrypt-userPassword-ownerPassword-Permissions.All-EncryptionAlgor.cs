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
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Encrypt using AES‑256 (KeySize.x256 + Algorithm.AES) and allow printing
            fileSecurity.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,   // set desired privileges
                KeySize.x256,
                Algorithm.AES);

            // Persist the encrypted PDF (optional – the constructor already defines the output file)
            fileSecurity.Save(outputPath);

            Console.WriteLine($"Encryption successful. Encrypted file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during encryption: {ex.Message}");
        }
    }
}