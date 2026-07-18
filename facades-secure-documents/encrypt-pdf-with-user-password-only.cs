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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Encrypt using only a user password; owner password is left undefined (null)
            bool encrypted = fileSecurity.EncryptFile(
                userPassword,          // user password
                null,                  // owner password (null => random generated)
                DocumentPrivilege.Print, // set desired privileges
                KeySize.x256);         // use strong 256‑bit encryption

            Console.WriteLine(encrypted
                ? $"Encryption succeeded. Output saved to '{outputPath}'."
                : "Encryption failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}