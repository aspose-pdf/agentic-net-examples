using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        Document doc = new Document(inputPath);

        // Define the permissions – allow printing only
        Permissions permissions = Permissions.PrintDocument;

        // Encrypt the document with both user and owner passwords
        // Using AES‑256 encryption (you can switch to AES‑128 if required)
        doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

        // Save the encrypted PDF
        doc.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
