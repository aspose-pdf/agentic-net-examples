using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

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

        // Restrict all permissions (no printing, no copying, etc.)
        Permissions permissions = (Permissions)0; // no permissions granted

        using (Document document = new Document(inputPath))
        {
            document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);
            document.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}