using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_aes128.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and apply AES‑128 encryption with high‑quality printing permission
        using (Document doc = new Document(inputPath))
        {
            // Permissions is a class with integer constants; combine them and cast to Permissions
            Permissions perms = (Permissions)(Permissions.PrintDocument | Permissions.PrintingQuality);
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
            doc.Save(encryptedPath);
        }

        // Open the encrypted PDF to verify that the high‑quality printing permission is present
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // The Permissions property returns an int; cast it back to the Permissions type for flag checks
            Permissions currentPerms = (Permissions)encDoc.Permissions;
            bool highQualityPrint = (currentPerms & Permissions.PrintingQuality) == Permissions.PrintingQuality;
            Console.WriteLine($"High‑quality printing permission enabled: {highQualityPrint}");
        }
    }
}
