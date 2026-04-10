using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document

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

        // Open the PDF, apply RC4‑128 encryption and restrict editing (no modify permissions)
        using (Document doc = new Document(inputPath))
        {
            // Allow only printing; omit ModifyContent to prevent editing
            Permissions perms = Permissions.PrintDocument;

            // RC4 128‑bit encryption
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
