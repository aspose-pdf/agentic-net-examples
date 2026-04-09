using System;
using System.IO;
using Aspose.Pdf;               // Core API (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted_rc4.pdf";
        const string decryptedPath  = "decrypted.pdf";

        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the original PDF and encrypt it using RC4 (128‑bit) while
        //    disabling the copy/extract permission.
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Allow printing only; do NOT include ExtractContent (copy) permission.
            Permissions permissions = Permissions.PrintDocument;

            // Encrypt with RC4‑128 algorithm.
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted document.
            doc.Save(encryptedPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");

        // -----------------------------------------------------------------
        // 2. Verify the security settings:
        //    - Open the encrypted file with the owner password (full access).
        //    - Decrypt it to ensure the passwords are correct.
        //    - (Optional) Re‑save the decrypted version.
        // -----------------------------------------------------------------
        using (Document encryptedDoc = new Document(encryptedPath, ownerPassword))
        {
            // Decrypt the document (no parameters required).
            encryptedDoc.Decrypt();

            // Save the decrypted copy (optional verification output).
            encryptedDoc.Save(decryptedPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        Console.WriteLine("Encryption and verification completed successfully.");
    }
}