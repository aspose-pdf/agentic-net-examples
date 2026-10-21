using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document doc = new Document(inputPath);

        // Define the permissions you want to allow.
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Apply password protection using AES‑256 encryption.
        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

        // Save the encrypted PDF to a new file.
        doc.Save(outputPath);

        Console.WriteLine($"Password‑protected PDF saved to '{outputPath}'.");
    }
}
