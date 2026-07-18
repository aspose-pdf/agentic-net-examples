using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed.pdf";
        const string outputPath = "signed_privileged.pdf";

        // Owner password is required for setting permissions; user password can be empty if not needed.
        const string userPassword  = "";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define permissions that exclude content extraction and form filling.
        // Here we allow only printing; other permissions can be added as needed.
        Permissions perms = Permissions.PrintDocument;

        using (Document doc = new Document(inputPath))
        {
            // Apply encryption with the specified permissions using AES-256.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Privileges applied and saved to '{outputPath}'.");
    }
}