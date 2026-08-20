using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, apply encryption with the core Document API, and save.
        using (Document doc = new Document(inputPath))
        {
            // Allow printing and content modification, but do NOT allow content extraction (copy‑paste).
            Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent;

            // Encrypt the document using AES‑256 (or any supported algorithm).
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'. Copy‑paste is disabled.");
    }
}
