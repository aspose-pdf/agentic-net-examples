using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted_aes128.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF, encrypt with AES‑128 and high‑quality printing permission, then save.
        using (Document doc = new Document(inputPath))
        {
            // Combine PrintDocument and PrintingQuality flags for high‑quality printing.
            Permissions perms = (Permissions)(Permissions.PrintDocument | Permissions.PrintingQuality);

            // Encrypt using AES‑128 (CryptoAlgorithm.AESx128).
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

            // Save the encrypted PDF.
            doc.Save(encryptedPath);
        }

        // Open the encrypted PDF with the user password and verify the permissions.
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            // Retrieve the permissions stored in the encryption dictionary.
            Permissions actualPerms = (Permissions)encryptedDoc.Permissions;

            Console.WriteLine("Encryption verification:");
            Console.WriteLine($" - PrintDocument permission: {(actualPerms.HasFlag(Permissions.PrintDocument) ? "Enabled" : "Disabled")}");
            Console.WriteLine($" - PrintingQuality permission: {(actualPerms.HasFlag(Permissions.PrintingQuality) ? "Enabled (high quality)" : "Disabled")}");
        }
    }
}
