using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // ---------- Encrypt ----------
            // Load the original PDF, apply encryption, and save the encrypted file.
            using (Document doc = new Document(inputPath))
            {
                // Define permissions (e.g., allow printing and content extraction).
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
                doc.Save(encryptedPath);
            }

            // ---------- Decrypt ----------
            // Open the encrypted PDF with the user password, decrypt it, and save the decrypted version.
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt the document (no parameters required).
                encryptedDoc.Decrypt();

                // Save the decrypted PDF.
                encryptedDoc.Save(decryptedPath);
            }

            Console.WriteLine("Encryption and decryption completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}