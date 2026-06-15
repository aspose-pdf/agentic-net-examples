using System;
using System.IO;
using Aspose.Pdf;               // Core API (contains Document, Permissions, CryptoAlgorithm)

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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF, apply AES‑256 encryption with no printing.
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Permissions flag set to 0 => no permissions (including no printing)
            Permissions noPermissions = (Permissions)0;

            // Encrypt using AES‑256 (CryptoAlgorithm) – the recommended enum.
            doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.AESx256);

            // Save the encrypted document.
            doc.Save(encryptedPath);
        }

        // -----------------------------------------------------------------
        // 2. Verify that the document is encrypted.
        // -----------------------------------------------------------------
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            // IsEncrypted reports true for encrypted PDFs.
            bool isEncrypted = encryptedDoc.IsEncrypted;
            Console.WriteLine($"Document encrypted: {isEncrypted}");

            // Since Aspose.Pdf does not expose a direct getter for permissions,
            // we infer the lack of printing permission by the absence of the
            // PrintDocument flag in the permissions we originally set (0).
            // Attempting to open with the user password succeeded, confirming
            // that the encryption parameters are valid.
        }
    }
}