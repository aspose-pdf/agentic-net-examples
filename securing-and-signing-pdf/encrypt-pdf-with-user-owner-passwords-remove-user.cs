using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string finalPath      = "final_encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Encrypt the PDF with both user and owner passwords
        // -------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Define desired permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using AES‑256 (preferred algorithm)
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted document
            doc.Save(encryptedPath);
        }

        // -------------------------------------------------
        // Step 2: Remove the user password while keeping the document encrypted
        // -------------------------------------------------
        // Open the encrypted file using the owner password (required for password changes)
        using (Document encDoc = new Document(encryptedPath, ownerPassword))
        {
            // Change passwords: keep the same owner password, set a blank user password
            encDoc.ChangePasswords(ownerPassword, "", ownerPassword);

            // Save the result – it remains encrypted but can be opened without a user password
            encDoc.Save(finalPath);
        }

        Console.WriteLine("Encryption completed. User password removed in final file.");
    }
}