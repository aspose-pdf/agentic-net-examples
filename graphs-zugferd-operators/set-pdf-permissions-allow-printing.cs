using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set permissions (allow printing only), encrypt, and save.
        using (Document doc = new Document(inputPath))
        {
            // Permissions: allow printing, disallow all other actions.
            Permissions perms = Permissions.PrintDocument;

            // Encrypt using AES-256 (preferred algorithm).
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with restricted permissions to '{outputPath}'.");
    }
}