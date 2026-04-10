using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured.pdf";

        // Passwords for the encrypted PDF
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF, apply encryption with the desired permissions, and save.
        using (Document doc = new Document(inputPath))
        {
            // Allow printing only. Do NOT include ExtractContent or ModifyTextAnnotations.
            Permissions perms = Permissions.PrintDocument;

            // Encrypt using AES‑256 (recommended) and the specified passwords.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}