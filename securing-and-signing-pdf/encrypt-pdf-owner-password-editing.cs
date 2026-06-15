using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Allow viewing, printing and content extraction, but prevent any modifications.
        Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

        // Open the PDF, apply encryption with an empty user password (no password needed to open)
        // and an owner password required for editing. Use AES‑256 for strong encryption.
        using (Document doc = new Document(inputPath))
        {
            doc.Encrypt(string.Empty, ownerPassword, permissions, CryptoAlgorithm.AESx256);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}