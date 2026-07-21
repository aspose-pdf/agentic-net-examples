using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, apply AES‑128 encryption with high‑quality printing permission, and save.
        using (Document doc = new Document(inputPath))
        {
            // Combine permission flags and cast the result to the Permissions type.
            Permissions perms = (Permissions)(Permissions.PrintDocument | Permissions.PrintingQuality);
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
            doc.Save(encryptedPath);
        }

        // Open the encrypted PDF with the user password and verify the permission flag.
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // The Permissions property returns an int; cast it back to Permissions for flag checks.
            Permissions actualPerms = (Permissions)encDoc.Permissions;
            bool highQualityPrint = (actualPerms & Permissions.PrintingQuality) == Permissions.PrintingQuality;
            Console.WriteLine($"High‑quality printing enabled: {highQualityPrint}");
        }
    }
}
