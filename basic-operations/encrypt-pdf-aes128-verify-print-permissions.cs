using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Added for Permissions enum

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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Encrypt the PDF ----------
        // Permissions: high‑quality printing only (using the available PrintDocument flag)
        Permissions perms = Permissions.PrintDocument;

        // AES‑128 encryption (CryptoAlgorithm.AESx128)
        using (Document doc = new Document(inputPath))
        {
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
            doc.Save(encryptedPath);
        }

        Console.WriteLine($"Document encrypted with AES‑128 and saved to '{encryptedPath}'.");

        // ---------- Verify encryption settings ----------
        // Open the encrypted file with the user password
        using (Document encDoc = new Document(encryptedPath, userPassword))
        {
            // Verify that the document is indeed encrypted (owner password is required for full access)
            bool isEncrypted = encDoc.IsEncrypted;
            Console.WriteLine($"IsEncrypted: {isEncrypted}");

            // Verify the applied permissions
            Permissions appliedPerms = (Permissions)encDoc.Permissions; // cast to Permissions enum
            bool hasPrintPermission = (appliedPerms & Permissions.PrintDocument) == Permissions.PrintDocument;
            Console.WriteLine($"PrintDocument permission set: {hasPrintPermission}");

            // Note: The exact encryption algorithm cannot be read directly via the API; successful decryption implies the algorithm matches the one used.
        }
    }
}
