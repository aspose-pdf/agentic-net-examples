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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            Permissions permissions = Permissions.PrintDocument | Permissions.ModifyContent;
            document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);
            document.Save(outputPath);
        }

        Console.WriteLine($"PDF encrypted with RC4‑128 and saved to '{outputPath}'.");
    }
}