using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt using 256‑bit AES and allow printing
        bool result = security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256, Algorithm.AES);

        if (result)
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Encryption failed.");
    }
}