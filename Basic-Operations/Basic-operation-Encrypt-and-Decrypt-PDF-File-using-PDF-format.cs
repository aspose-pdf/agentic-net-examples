using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the original, encrypted, and decrypted PDF files
        const string inputPath     = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // Passwords for encryption/decryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Not found: {inputPath}");
            return;
        }

        try
        {
            // ---------- Encrypt ----------
            // Load the original PDF
            using (Document doc = new Document(inputPath))
            {
                // Set desired permissions (e.g., allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply encryption with AES-256 algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // ---------- Decrypt ----------
            // Open the encrypted PDF using the user password
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                // Decrypt the document (no parameters needed)
                encDoc.Decrypt();

                // Save the decrypted PDF
                encDoc.Save(decryptedPath);
            }

            Console.WriteLine("Encrypt/decrypt completed.");
        }
        // Handle incorrect password errors
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Wrong password: {ex.Message}");
        }
        // Handle any other unexpected errors
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}