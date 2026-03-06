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
            using (Document doc = new Document(inputPath))
            {
                // Set desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with AES-256 algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // ---------- Decrypt ----------
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt the document (no parameters)
                encDoc.Decrypt();

                // Save the decrypted PDF (overwrites the encrypted file or to a new file)
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