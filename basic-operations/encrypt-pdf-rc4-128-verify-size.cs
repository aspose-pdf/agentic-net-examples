using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_rc4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Record original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Encrypt using 128‑bit RC4
        using (Document doc = new Document(inputPath))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(
                userPassword: "",               // no user password
                ownerPassword: "owner123",     // owner password
                permissions: perms,
                cryptoAlgorithm: CryptoAlgorithm.RC4x128);
            doc.Save(encryptedPath);
        }

        // Record encrypted file size
        long encryptedSize = new FileInfo(encryptedPath).Length;

        Console.WriteLine($"Original size:   {originalSize} bytes");
        Console.WriteLine($"Encrypted size: {encryptedSize} bytes");

        if (encryptedSize > originalSize)
        {
            Console.WriteLine($"File size increased by {encryptedSize - originalSize} bytes.");
        }
        else
        {
            Console.WriteLine("Encrypted file is not larger than the original (unexpected).");
        }
    }
}