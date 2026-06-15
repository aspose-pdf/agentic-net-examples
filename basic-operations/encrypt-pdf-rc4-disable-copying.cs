using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted_rc4.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Encrypt the PDF using RC4 (128‑bit) and disable all copy permissions
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // No permissions granted (copying is disabled)
            Permissions noPermissions = (Permissions)0;

            // RC4 128‑bit algorithm
            doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted document
            doc.Save(encryptedPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");

        // -----------------------------------------------------------------
        // Verify that the document is encrypted and can be opened with the
        // user password, then decrypt it to confirm the security settings.
        // -----------------------------------------------------------------
        try
        {
            // Open with the user password
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                // If we reach this point the password is correct and the file is
                // recognized as encrypted.
                Console.WriteLine("Document opened successfully with user password.");

                // Decrypt the document (no parameters) and save the decrypted copy
                encryptedDoc.Decrypt();
                string decryptedPath = "decrypted_rc4.pdf";
                encryptedDoc.Save(decryptedPath);
                Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
            }
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Failed to open the encrypted PDF with the provided password.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}