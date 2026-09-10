using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document

class Program
{
    static void Main()
    {
        const string inputPath     = "input.pdf";
        const string encryptedPath = "encrypted.pdf";

        // Passwords for user (open) and owner (change permissions)
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Set permissions: allow printing and copying (extracting content)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}