using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_rc4x128.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the original PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using 128‑bit RC4 algorithm
            doc.Encrypt(
                userPassword: "user123",
                ownerPassword: "owner123",
                permissions: perms,
                cryptoAlgorithm: CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
        }

        // Compare file sizes to verify that encryption increased the file size slightly
        long originalSize = new FileInfo(inputPath).Length;
        long encryptedSize = new FileInfo(encryptedPath).Length;

        Console.WriteLine($"Original size:  {originalSize} bytes");
        Console.WriteLine($"Encrypted size: {encryptedSize} bytes");

        if (encryptedSize > originalSize)
        {
            Console.WriteLine("File size increased after encryption (as expected).");
        }
        else
        {
            Console.WriteLine("File size did not increase; encryption may not have been applied correctly.");
        }
    }
}