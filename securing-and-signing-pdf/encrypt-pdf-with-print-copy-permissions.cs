using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF and encrypt it with the desired permissions.
        using (Document doc = new Document(inputPath))
        {
            // Allow printing and copying (extracting content).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using AES-256 (recommended) and the specified passwords.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(encryptedPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
    }
}