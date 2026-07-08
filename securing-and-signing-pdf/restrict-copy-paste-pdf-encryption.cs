using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Allow only printing; omit ExtractContent to block copy‑paste
        Permissions permissions = Permissions.PrintDocument;

        // Load, encrypt with built‑in security handler, and save
        using (Document doc = new Document(inputPath))
        {
            // Use AES 128‑bit encryption (change to AESx256 if stronger encryption is required)
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx128);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with security to '{outputPath}'.");
    }
}
