using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "viewonly";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Allow printing but disallow content extraction and modification
            Permissions perms = Permissions.PrintDocument;

            // Owner password can be empty; user password is required for opening
            string ownerPassword = "";

            // Encrypt with AES‑256 algorithm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}