using System;
using System.IO;
using Aspose.Pdf;

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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, apply encryption allowing only form filling, then save.
        using (Document doc = new Document(inputPath))
        {
            // Permissions: only FillForm (no content extraction, no printing, etc.)
            Permissions perms = Permissions.FillForm;

            // Encrypt with AES-256 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}