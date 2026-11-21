using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted_rc4_40.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Set desired permissions (example: allow printing only)
        Permissions perms = Permissions.PrintDocument;

        // Encrypt using RC4‑40 algorithm with the specified passwords
        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x40);

        // Save the encrypted PDF
        doc.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}' using RC4‑40.");
    }
}
