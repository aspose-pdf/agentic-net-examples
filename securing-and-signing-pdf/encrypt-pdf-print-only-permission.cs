using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted_print_only.pdf";

        // Passwords for the encrypted PDF
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF, apply encryption with only printing permission, and save
        using (Document doc = new Document(inputPath))
        {
            // Allow only printing (no other operations)
            Permissions printOnly = Permissions.PrintDocument;

            // Prefer AES-256 encryption algorithm
            doc.Encrypt(userPassword, ownerPassword, printOnly, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}' with print‑only permission.");
    }
}