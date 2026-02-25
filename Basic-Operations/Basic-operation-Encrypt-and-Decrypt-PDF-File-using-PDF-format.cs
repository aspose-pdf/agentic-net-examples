using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document

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
            // Load the original PDF, apply encryption, and save.
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
            }

            // ---------- Decrypt ----------
            // Open the encrypted PDF with the user password, decrypt, and save.
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                encDoc.Decrypt(); // No parameters required.
                encDoc.Save(decryptedPath);
            }

            Console.WriteLine("Encryption and decryption completed successfully.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}