using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string finalPath      = "final.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Step 1: Encrypt with both user and owner passwords ----------
        using (Document doc = new Document(inputPath))
        {
            // Define permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using AES-256 (preferred algorithm)
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(encryptedPath);
        }

        // ---------- Step 2: Remove the user password while preserving encryption ----------
        // Open the encrypted file using the owner password (required for ChangePasswords)
        using (Document encDoc = new Document(encryptedPath, ownerPassword))
        {
            // Change passwords:
            // - Keep the same owner password
            // - Set the new user password to an empty string (removes user password)
            encDoc.ChangePasswords(ownerPassword, string.Empty, ownerPassword);

            // Save the resulting PDF (now only protected by the owner password)
            encDoc.Save(finalPath);
        }

        Console.WriteLine($"Encrypted PDF saved to: {encryptedPath}");
        Console.WriteLine($"User password removed; final PDF saved to: {finalPath}");
    }
}