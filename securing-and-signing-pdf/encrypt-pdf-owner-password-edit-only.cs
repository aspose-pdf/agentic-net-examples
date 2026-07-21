using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF, apply encryption, and save.
        using (Document doc = new Document(inputPath))
        {
            // Allow viewing, printing, and content extraction, but prevent modifications.
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // No user password (empty string) → PDF can be opened for viewing without a password.
            // Owner password protects editing operations (changing permissions, etc.).
            doc.Encrypt(string.Empty, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}