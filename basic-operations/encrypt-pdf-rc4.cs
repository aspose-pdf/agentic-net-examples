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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        long originalSize = new FileInfo(inputPath).Length;

        using (Document doc = new Document(inputPath))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt("user123", "owner123", perms, CryptoAlgorithm.RC4x128);
            doc.Save(encryptedPath);
        }

        long encryptedSize = new FileInfo(encryptedPath).Length;
        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Encrypted size: {encryptedSize} bytes");
        if (encryptedSize > originalSize)
            Console.WriteLine("File size increased after encryption.");
        else
            Console.WriteLine("File size did not increase.");
    }
}
