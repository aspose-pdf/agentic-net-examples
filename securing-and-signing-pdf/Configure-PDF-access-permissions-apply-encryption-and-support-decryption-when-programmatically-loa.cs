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

        // ---------- Encrypt the PDF ----------
        // Load the original document, set permissions, apply encryption, and save.
        using (Document doc = new Document(inputPath))
        {
            // Define allowed operations for the user (e.g., print and extract content).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using user/owner passwords and AES-256 algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(encryptedPath);
        }

        // ---------- Decrypt the PDF ----------
        // Open the encrypted document with the user password, decrypt, and save the clear version.
        using (Document encryptedDoc = new Document(encryptedPath, userPassword))
        {
            // Decrypt the document (no parameters required).
            encryptedDoc.Decrypt();

            // Save the decrypted PDF.
            encryptedDoc.Save(decryptedPath);
        }

        Console.WriteLine("Encryption and decryption completed successfully.");
    }
}