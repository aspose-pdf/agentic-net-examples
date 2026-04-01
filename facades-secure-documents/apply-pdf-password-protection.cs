using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF without recreating it from scratch
        Document doc = new Document(inputPath);

        // Define the permissions you want to allow
        Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent;

        // Apply password protection using the modern API
        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

        // Save the protected PDF
        doc.Save(outputPath);
        Console.WriteLine($"Password‑protected PDF saved to '{outputPath}'.");
    }
}
