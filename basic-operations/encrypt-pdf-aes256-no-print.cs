using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF, apply AES‑256 encryption with no permissions (no printing)
        using (Document doc = new Document(inputPath))
        {
            // No permissions granted (PrintDocument flag omitted)
            Permissions perms = 0; // equivalent to no rights
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
        }

        // Verify that the document is encrypted
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            Console.WriteLine($"IsEncrypted: {encryptedDoc.IsEncrypted}");
            // Since no permissions were set, printing is not allowed.
            // Aspose.Pdf does not expose a direct permission check; the absence of the flag confirms it.
        }

        Console.WriteLine($"Encryption completed. Encrypted file saved as '{encryptedPath}'.");
    }
}