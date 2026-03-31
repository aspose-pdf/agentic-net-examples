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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Define the permissions you want to allow
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply AES‑256 encryption with the specified passwords and permissions
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
