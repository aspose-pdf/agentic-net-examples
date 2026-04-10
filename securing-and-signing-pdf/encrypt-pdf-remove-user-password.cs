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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Encrypt the PDF with both user and owner passwords
        // -------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Define desired permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using AES-256 (preferred algorithm)
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted document
            doc.Save(encryptedPath);
        }

        // -------------------------------------------------
        // Step 2: Remove the user password while keeping the document encrypted
        // -------------------------------------------------
        // Open the encrypted PDF using the owner password (required for password changes)
        using (Document encDoc = new Document(encryptedPath, ownerPassword))
        {
            // Change passwords:
            // - Provide the current owner password for authentication
            // - Set the new user password to an empty string (removes it)
            // - Keep the owner password unchanged
            encDoc.ChangePasswords(ownerPassword, string.Empty, ownerPassword);

            // Save the resulting PDF (still encrypted, but without a user password)
            encDoc.Save(finalPath);
        }

        Console.WriteLine("PDF encrypted and user password removed successfully.");
    }
}